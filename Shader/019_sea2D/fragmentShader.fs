#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;
#define LAYER 19.0


float Circle(vec2 uv,vec2 center,float size,float blur){
    uv = uv - center;
    uv /= size;
    float len = length(uv);
    return smoothstep(1.,1.-blur,len);
}
//简单直观的合成方式
float DrawCloud(vec2 uv,vec2 center,float size){
    uv = uv - center;
    uv /= size;
    float col = Circle(uv,vec2(0.,0.),0.2,0.05);
    col =col *  smoothstep(-0.1,-0.1+0.01,uv.y);//将圆中不想要的的部分给剪切掉
    col += Circle(uv,vec2(0.15,-0.05),0.1,0.05);
    col += Circle(uv,vec2(0.010,-0.060),0.11,0.05);
    col += Circle(uv,vec2(-0.15,-0.1),0.1,0.05);
    col += Circle(uv,vec2(-0.3,-0.08),0.1,0.05);
    col += Circle(uv,vec2(-0.2,0.),0.15,0.05);
    return col;
}
float DrawClouds(vec2 uv){
    uv.x += 0.03*u_time;
    uv.x = fract(uv.x+0.5) - 0.5;
    float col = DrawCloud( uv,vec2(-0.4,0.3),0.2);
    col += DrawCloud( uv,vec2(-0.2,0.42),0.2);
    col += DrawCloud( uv,vec2(0.0,0.4),0.2);
    col += DrawCloud( uv,vec2(0.15,0.3),0.2);
    col += DrawCloud( uv,vec2(0.45,0.45),0.2);
    return col;
}

float AngleCircle(vec2 uv,vec2 center,float size,float blur){
    uv = uv - center;
    uv /= size;
    float deg = atan(uv.y,uv.x) + u_time * -0.1;//转化为极坐标
    float len = length(uv);//转化为极坐标
    //通过极坐标角度的方式来绘制波浪型的圆环
    float offs =( sin(deg*9.)*3.+sin(deg*11.+sin(u_time*6.0)*.5))*0.05;
    return smoothstep(1.+offs,1.-blur+offs,len);
}

 float Wave(float layer,vec2 uv,float val){
    float amplitude =  layer*layer*0.00009;//这些数值都是为了美术效果  怎么漂亮怎么来
    float frequency = val*199.696*uv.x/layer;
    float phase = 9.*layer+ u_time/val;
    return amplitude*sin(frequency+phase); 
}

float Remap(float oa,float ob,float na,float nb,float val){
	return (val-oa)/(ob-oa) * (nb-na) + na;
}

void main(void){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;
    
    vec3 col = vec3(0.0,0.0,0.0);
    float num = 0.;
    for (float i=1.; i < LAYER; i++) {
        //类似FBM的叠加方式，没加一层整幅下降一半，平率提升两倍左右，目的是
        //既可以用第一个函数控制大概的形状，又可以增加后面的函数添加高频的变化，方便控制细节
        //同样这些参数 是为了让函数的形状变得好看
        float wave = 2.*Wave(i,uv,1.)+Wave(i,uv,1.8)+.5*Wave(i,uv,3.);
        float layerVal = 0.7-0.03*i + wave;//控制波浪的高度
        if(uv.y > layerVal){
            break;
        }
        num = i;//计算所在层的ID
    }
    col = num*vec3(0,.03,1);//计算每一层的基本颜色
    col += (LAYER - num) * vec3(.04,.04,.04);//颜色叠亮
    //在最高的一层海浪之上
    if(num == 0.){
        //添加海平面泛光
        float ry = Remap(0.708,1.024,1.0,0.0,uv.y);//0.7是最高海浪值的水平面
        col = mix(vec3(0.1,0.6,0.9),vec3(0.1,0.7,0.9),ry);//简单的颜色渐变
        col += pow(ry,10.)*vec3(0.9,0.2,0.1)*0.2;//让接近海平面的地方泛白 pow是为了控制影响范围
    }
    gl_FragColor = vec4(col,1.0);
}
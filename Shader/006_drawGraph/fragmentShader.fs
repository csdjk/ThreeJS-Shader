
// Author: 长生但酒狂
// create time : 2019-12-15
// Title：结合不同的【造型函数】 绘制图案, 并且封装成函数,可以绘制多个、实现遮罩等效果

// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

// hsb 转 rgb
vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0,0.0,1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix( vec3(1.0), rgb, c.y);
}

// 圆
vec3 createCircle(vec2 pos,float radius,vec2 uv){
	float pct = distance(pos,uv);
    //  smoothstep 平滑过渡
    return vec3(smoothstep(radius + 0.008,radius,pct )) ;
}
// 圆环，其实就是两个圆相减
vec3 createAnnulus(vec2 pos,float radius,float width,vec2 uv){
	float dis = distance(pos,uv);
    float col = smoothstep(radius, radius+0.010,dis) - smoothstep(radius+width, radius+width+0.010,dis);
    return vec3(col);
}

// 花瓣
vec3 createPetal(vec2 pos,float scale,vec2 uv){
    vec2 dir = pos - uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
	//造型函数
    float f = abs(cos(angle*2.5))*.5+.3;
    return vec3( 1.-smoothstep(f,f+0.02,radius));
}
// 水滴
vec3 createWaterDrop(vec2 pos,float scale,vec2 uv){
    vec2 dir = pos - uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
	//造型函数
    float f = 1.0 - pow(abs(angle+-1.548),2.416);
    return vec3( 1.-smoothstep(f,f+0.02,radius));
}

// 风车
vec3 createWindmill(vec2 pos,float scale,vec2 uv){
	 vec2 dir = pos-uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
    
    float f = fract(angle*1.273 + u_time)*0.956;
    return vec3( 1.-smoothstep(f,f+0.092,radius) );
}

// 齿轮
vec3 createGear(vec2 pos,float scale,vec2 uv){
    vec2 dir = pos - uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
	//造型函数
   	float f = smoothstep(-0.484,1., cos(angle*10.0+u_time))*0.080+0.372;

    return vec3( 1.-smoothstep(f,f+0.02,radius));
}
// 创建...
vec3 createD(vec2 pos,float scale,vec2 uv){
    vec2 dir = pos - uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
  	 //造型函数
   	float f = abs(cos(angle*13.136)*sin(angle*3.))*.8+.1;
    return vec3( 1.-smoothstep(f,f+0.02,radius));
}

// 心形
vec3 createHeartShape(){
    vec2 pos = (3.0*gl_FragCoord.xy-u_resolution.xy)/min(u_resolution.y,u_resolution.x);
    float a = atan(pos.x,pos.y)/3.141593;
    float r = length(pos);
    float h = abs(a);
    float d = (13.0*h - 22.0*h*h + 10.0*h*h*h)/(6.0-5.0*h);
    float s = clamp(r/d,1.0,1.1);
    s *= pow( 1.0-r/d, 0.1);
    vec3 col = vec3(1.0,0.,0.)*s;
    return col;
}

void main(){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;

    vec3 color = createGear(vec2(0.800,0.480),5.0,uv);
    vec3 color1 = createD(vec2(0.310,0.630),5.0,uv);
    vec3 color2 = createCircle(vec2(0.500,0.450),0.072,uv);
    vec3 color3 = createPetal(vec2(0.280,0.220),5.352,uv);
    vec3 color4 = createWindmill(vec2(0.630,0.790),5.352,uv);
    vec3 color5 = createWaterDrop(vec2(0.820,0.250),5.352,uv);

    // color = color * hsb2rgb(vec3(0.541,1.117,0.353));

    //注意这里:
    //"+" 代表 "||(或者)" 的意思, 即 color1 + color2 代表 只要该像素在color1或者color2其中一个上，就显示，否则不显示。【显示多个】
    //"-" 代表 "与非 "  的意思, 即 color1 - color2 代表 如果该像素在color1上,并且不在color2上,则显示,否则不显示。【color1减掉color2区域颜色】
    //"*" 代表 "&&(与)" 的意思, 即 color1 * color2 代表 如果该像素在既在color1上又在color2上，就显示，否则不显示。 【遮罩效果】
    gl_FragColor = vec4(color+color1+color2+color3+color4+color5, 1.0);
}

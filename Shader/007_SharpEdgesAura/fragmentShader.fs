
// Author: 长生但酒狂
// create time : 2019-12-16
// Title：尖刃光环

// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

// 圆环
vec3 createAnnulus(vec2 pos,float radius,float width,vec2 uv){
	float dis = distance(pos,uv);
    float col = smoothstep(radius, radius+0.010,dis) - smoothstep(radius+width, radius+width+0.010,dis);
    return vec3(col);
}

void main(void){
    vec2 p = (3.0*gl_FragCoord.xy-u_resolution.xy)/min(u_resolution.y,u_resolution.x);

    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;

    vec3 color = vec3(0.0);
	
    vec2 center = vec2(0.500,0.500);
    float radius = 0.308;//半径
    float width = 0.012;//圆环宽度
    float gap = 1.392; //尾部缺口
   
	//尾部缺口
    //上方向向量
    vec2 upDir = normalize(vec2(0.5,1.) - center);
	//当前向量 
    vec2 currentDir = normalize(uv - center);
    //角度
    float angle = dot(upDir,currentDir);
    float tailRang = smoothstep(gap,gap+0.284,angle);
	 float s = (1. - pow(abs(angle),1.));
    width = width * s ;
     //圆环
    vec3 col = createAnnulus(center,radius,width,uv);
    
    gl_FragColor = vec4(vec3(col),1.0);
}

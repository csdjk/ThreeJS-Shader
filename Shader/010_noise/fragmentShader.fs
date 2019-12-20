// Author: 长生但酒狂
// create time : 2019-12-18
// Title：切割格子

// ------------------------------【片元着色器】----------------------------

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;


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
	
    vec2 center = vec2(0.5,0.5);
    float radius = 0.3;//半径
    float width = 0.020;//圆环宽度
    float gap = 0.608; //尾部缺口
    
    
   
	//尾部缺口
    vec2 upDir = normalize(vec2(0.5,1.) - center);
    vec2 currentDir = normalize(uv - center);
    
    float angle = dot(upDir,currentDir);
    float tailRang = smoothstep(gap,gap+0.020,angle);
	 float s = (1. - pow(abs(angle),1.)) * (1.-tailRang);
    width = width * (1.-tailRang) * s ;
     //圆环
    vec3 col = createAnnulus(center,radius,width,uv);
    
    
    col = col;


    gl_FragColor = vec4(vec3(col),1.0);
}
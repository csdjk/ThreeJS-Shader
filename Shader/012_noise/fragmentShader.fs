// Author:
// Title:

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;
// 圆
vec3 createCircle(vec2 pos,float radius,vec2 uv){
	float pct = distance(pos,uv);
    //  smoothstep 平滑过渡
    return vec3(smoothstep(radius + 0.008,radius,pct )) ;
}

void main() {
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;
    //鼠标位置
	 vec2 mousePos = u_mouse/u_resolution;
    float isOnScreen =  step(0.001,mousePos.x) + step(0.001,mousePos.y);
    
    vec3 color = vec3(0.);
	// 中心
    vec2 center = isOnScreen == 0. ? vec2(0.5,0.5) : mousePos;
    //波纹宽度
    float width = 0.2;
    //波纹数量
    float rippleNum = 6.;
    //扩散速度
    float speed = 2.;
    //距离
    float dis = distance(center,uv);
    //动画
    float action = dis * rippleNum * 5. - u_time*speed ;
    //是否是波纹, 0 是波纹, 1 是背景
    float isRipple = smoothstep(width,width+0.05,abs(sin(action)));

 	//背景颜色
	color += vec3(dis,0.504,0.960) * isRipple;
    //波纹颜色
	color += vec3(0.637,0.803,0.980) * (1. - isRipple);
    
    gl_FragColor = vec4(color,1.0);
}
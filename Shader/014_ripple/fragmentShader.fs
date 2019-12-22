// Author: 长生但酒狂
// create time : 2019-12-21
// Title：波纹

// ------------------------------【片元着色器】----------------------------
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
    float isOnScreen =  step(0.01,mousePos.x) * step(0.01,mousePos.y);

    vec3 color = vec3(0.);
	// 中心
    // vec2 center = vec2(0.5,0.5) * (1.-isOnScreen) + mousePos *isOnScreen  ;
    vec2 center = vec2(0.5,0.5);

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
// Author: 长生但酒狂
// create time : 2019-12-14 2:14
// Title：结合不同的【造型函数】 绘制图案

// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main(){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    vec3 color = vec3(0.0);

    vec2 pos = vec2(0.5)-uv;

    float r = length(pos)*2.0;
    float a = atan(pos.y,pos.x);

    float f = cos(a*3.);

    // f = abs(cos(a*3.));
	//花瓣
    // f = abs(cos(a*2.5))*.5+.3;

    // f = abs(cos(a*13.136)*sin(a*3.))*.8+.1;
	//齿轮
    // f = smoothstep(-.5,1., cos(a*10.))*0.2+0.5;
	//风车
    // f = fract(a*1.264)*0.956;

    // 水滴
	 f = 1.0 - pow(abs(a),2.416);


    color = vec3( 1.-smoothstep(f,f+0.02,r) );
    gl_FragColor = vec4(color, 1.0);
}


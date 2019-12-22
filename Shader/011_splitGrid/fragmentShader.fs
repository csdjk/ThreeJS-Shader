// Author: 长生但酒狂
// create time : 2019-12-18
// Title：分割网格

// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

void main(void){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;

    //放大3倍
    uv *= 3.;
    //取小数, 实际上就是分成了 3 组 [0 - 1]的浮点数
    vec2 f_uv = fract(uv);
    //3x3 的网格
    vec3 color = vec3(f_uv.x,f_uv.y,0.);

    gl_FragColor = vec4(color,1.0);
}
// Author: 长生但酒狂
// create time : 2019-12-14 22:00
// Title：绘制正方形
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

    // bottom-left
    vec2 bl = step(vec2(0.1),uv);
    float pct = bl.x * bl.y;

    // top-right
     vec2 tr = step(vec2(0.1),1.0-uv);
     pct *= tr.x * tr.y;

    color = vec3(pct);

    gl_FragColor = vec4(color,1.0);
}
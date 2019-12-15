// Author: 长生但酒狂
// create time : 2019-12-13 17:36
// Title：渐变色
// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕大小
uniform vec2 u_resolution;

void main() {
	vec2 uv = gl_FragCoord.xy/u_resolution;
	gl_FragColor = vec4(uv.x,uv.y,0.0,1.0);
}
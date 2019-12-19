// Author: 长生但酒狂
// create time : 2019-12-13 17:36
// Title：渐变色
// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量, 由js传递过来------
// 屏幕尺寸
uniform vec2 u_resolution;

// 
void main() {
	// uv坐标
	vec2 uv = gl_FragCoord.xy/u_resolution;
	// 输出
	gl_FragColor = vec4(uv.x,uv.y,0.0,1.0);
}
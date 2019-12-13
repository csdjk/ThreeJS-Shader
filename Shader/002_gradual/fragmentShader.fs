// create by 长生但酒狂
// create time : 2019-12-13 17:36
// 渐变
// ------------------------------【片元着色器】----------------------------
#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕坐标
uniform vec2 u_resolution;

void main() {
	vec2 st = gl_FragCoord.xy/u_resolution;
	gl_FragColor = vec4(st.x,st.y,0.0,1.0);
}
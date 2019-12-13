// create by 长生但酒狂
// create time : 2019-12-13 22:00
// ------------------------------【片元着色器】----------------------------
// GLSH 画布边界点

#ifdef GL_ES
precision mediump float;
#endif
//-------- uniform 变量------
// 屏幕坐标
uniform vec2 u_resolution;
uniform float u_time;

void main() {
	// gl_FragCoord: 当前像素坐标
	// 对坐标进行规范化，把坐标控制在[0,1]范围.
	vec2 st = gl_FragCoord.xy/u_resolution;
	// 线宽度
	float lineWidth = 0.005;
	//正弦函数
	//频率
	float frequency = 10.0;
	//震幅
	float amplitude = 0.2;
	float y = sin(st.x * frequency + u_time) * amplitude + 0.5;

	// -------------------【为优化版】----------------------
	// // 如果该像素的在这范围内,代表是该线
	// if(st.y <= y+lineWidth && st.y >= y-lineWidth){
	// 	gl_FragColor = vec4(0,1,1,1);
	// }else{
	// 	gl_FragColor = vec4(0,0,0,1);
	// }

	// -------------------【优化后】----------------------
	float col = step(lineWidth, abs(y-st.y));
	gl_FragColor = vec4(vec3(col),1);
}
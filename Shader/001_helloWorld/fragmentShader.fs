// Author: 长生但酒狂
// create time : 2019-12-13 17:26
// Title：第一个GLSH demo
// ------------------------------【片元着色器】----------------------------
// uniform 统一变量：由js传递过来
uniform float u_time;

void main() {
    // 片元颜色
	gl_FragColor = vec4(abs(sin(u_time)),0.0,0.0,1.0);
}
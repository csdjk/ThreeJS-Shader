// Author: 长生但酒狂
// create time : 2019-12-18
// Title：常用变换矩阵

// ------------------------------【片元着色器】----------------------------

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

// 旋转
mat2 rotate2d(float _angle){
    return mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle));
}


void main(void){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;

    vec3 color = vec3(0.0);

    gl_FragColor = vec4(vec3(color),1.0);
}
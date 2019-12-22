// Author: 长生但酒狂
// create time : 2019-12-18
// Title：常用变换矩阵

// ------------------------------【片元着色器】----------------------------

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;


// 花瓣
vec3 createPetal(vec2 pos,float scale,vec2 uv){
    vec2 dir = pos - uv;
    float radius = length(dir)*scale;
    float angle = atan(dir.y,dir.x);
	//造型函数
    float f = abs(cos(angle*2.5))*.5+.3;
    return vec3( 1.-smoothstep(f,f+0.02,radius));
}


// 旋转
mat2 rotate2d(float _angle){
    return mat2(
        		cos(_angle),-sin(_angle),
             sin(_angle),cos(_angle)
    			);
}

// 缩放
mat2 scale2d(float _scale){
    return mat2(
        	  _scale,0.,
            0.,_scale
    		);
}

// // 平移
// mat3 translate2d(vec2 pos){
//     return mat3(
//         	  1.,0.,pos.x,
//             0.,1.,pos.y,
//             0.,0.,1.
//     		);
// }

void main(void){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;

    //旋转之前 偏移到圆点
    uv -= vec2(0.5);
    //旋转
    uv = rotate2d(u_time) * uv;
    //旋转之后偏移回原位
    uv += vec2(0.5);

    //花瓣
    vec3 color = createPetal(vec2(0.5,0.5 ),5.,uv);

    gl_FragColor = vec4(vec3(color),1.0);
}
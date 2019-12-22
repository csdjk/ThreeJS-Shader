// Author: 长生但酒狂
// create time : 2019-12-18
// Title：切割(平铺)格子

// ------------------------------【片元着色器】----------------------------

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform float u_time;

// 分割格子 - Split the grid
vec2 splitGrid(vec2 _uv, float _zoom,float speed){
    _uv *= _zoom;

    float time = u_time * speed;
	// 是否该行运动,否则列运动
	float isRow = step(0.5,fract(time));

    // 每行的动作 - row action
	//单数行的运动
    _uv.x += step(1., mod(_uv.y,2.0)) * u_time * isRow;
	//双数行的运动
    _uv.x += step( mod(_uv.y,2.0),1.) * -u_time * isRow;

	// 每列的动作 - col action
    _uv.y += step(1., mod(_uv.x,2.))  * u_time * (1. - isRow);
    _uv.y += step( mod(_uv.x,2.),1.)  * -u_time * (1. - isRow);

    return fract(_uv);
}

//创建圆 - creator circle
vec3 createCircle(vec2 pos,float radius,vec2 uv){
	float pct = distance(pos,uv);
    //  平滑过渡 - smoothstep
    return vec3(smoothstep(radius + 0.040,radius,pct )) ;
}

void main(void){
    vec2 uv = gl_FragCoord.xy/u_resolution.xy;
    uv.x *= u_resolution.x/u_resolution.y;

    vec3 color = vec3(0.0);

    // 分割格子 - Split the grid
    uv = splitGrid(uv,10.0,0.5);

	//创建圆 - creator circle
    color = vec3(createCircle(vec2(0.5),0.4,uv));


    gl_FragColor = vec4(color,1.0);
}
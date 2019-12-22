// Author: @patriciogv
// Title: Simple Voronoi

//
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec2 random2( vec2 p ) {
    return fract(sin(vec2(dot(p,vec2(127.1,311.7)),dot(p,vec2(269.5,183.3))))*43758.5453);
}

void main() {
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    st.x *= u_resolution.x/u_resolution.y;
    vec3 color = vec3(.0);

    // Scale - 缩放 3 倍
    st *= 3.;

    // Tile the space - 分割空间
    vec2 i_st = floor(st);
    vec2 f_st = fract(st);

    float m_dist = 10.;  // minimun distance - 最小距离
    vec2 m_point;        // minimum point - 最近的点

    //循环遍历邻居格子
    for (int j=-1; j<=1; j++ ) {
        for (int i=-1; i<=1; i++ ) {
            //邻居格子
            vec2 neighbor = vec2(float(i),float(j));
            //网格中当前位置+相邻位置的随机位置
            vec2 point = random2(i_st + neighbor);
            //运动
            point = 0.5 + 0.5*sin(u_time + 6.2831*point);
            //当前像素 到 随机点 的距离
            vec2 diff = neighbor + point - f_st;
            float dist = length(diff);
				 //取最小距离
            if( dist < m_dist ) {
                //保存最小距离
                m_dist = dist;
                //保存该点
                m_point = point;
            }
        }
    }

    // Assign a color using the closest point position - 使用最近的点位置分配颜色
    color += dot(m_point,vec2(0.230,0.700));

    // Add distance field to closest point center - 向最近点中心添加距离场
    color.g = m_dist;

    // Show isolines - 显示等值线
    color -= abs(sin(40.0*m_dist))*0.07;

    // Draw cell center - 绘制细胞中心
    color += 1.-step(.05, m_dist);

    // Draw grid - 绘制网格
    color.r += step(.98, f_st.x) + step(.98, f_st.y);

    gl_FragColor = vec4(color,1.0);
}

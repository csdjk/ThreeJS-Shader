
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;
//最大光线检测次数
#define MAX_RAYCAST_STEPS 100
#define Max_Dist 100.	 
#define Surf_Dist 0.001

// 来源:http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
// 距离场函数 - 球
float sphereSDF( vec3 p, float s )
{
//  return length(p)-s;
  float dis = length(p)-s;
  return dis;
}
// 距离场函数 - 盒子
float boxSDF( vec3 p, vec3 b )
{
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}
// 距离场函数 - 圆环
float torusSDF( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

//距离场函数 - 场景(包含需要绘制的所有物体)
float sceneSDF(vec3 p) {
  float z = 10.;
  //盒子
  vec3 boxPos = vec3(3,2.,z);
  vec3 boxSize = vec3(0.6);
  float boxDist = boxSDF(p-boxPos,boxSize);// P点到box的距离
  //球
  vec3 spherePos = vec3(0,3,z);
  float sphereSize = 1.;
  float sphereDist = sphereSDF(p-spherePos,sphereSize);// P点到球面的距离
  //圆环
  vec3 torusPos = vec3(-3.0,2.,z);
  vec2 torusSize = vec2(.920,0.290);
  float torusDist = torusSDF(p-torusPos,torusSize);// P点到圆环面的距离
  
  //地面
  float planeDist  = p.y;// P点到地面的距离，平面是xz平面，高度y = 0；
  //融合距离场
  float d = min(sphereDist,planeDist);
  d = min(d,boxDist);
  d = min(d,torusDist);
  return d;
}


// 阴影
float SoftShadow(vec3 ro, vec3 rd )
{
    float res = 1.0;
    float t = 0.001;
    for( int i=0; i<50; i++ )
    {
        vec3  p = ro + t*rd;
        float h = sceneSDF(p);
        res = min( res, 16.0*h/t );
        t += h;
        if( res<0.001 ||p.y>(200.0) ) break;
    }
    return clamp( res, 0.0, 1.0 );
}

//计算法线
vec3 calcNormal( in vec3 p ) // for function f(p)
{
    const float eps = 0.0001; // or some other value
    const vec2 h = vec2(eps,0);
    return normalize( vec3(sceneSDF(p+h.xyy) - sceneSDF(p-h.xyy),
                           sceneSDF(p+h.yxy) - sceneSDF(p-h.yxy),
                           sceneSDF(p+h.yyx) - sceneSDF(p-h.yyx) ) 
                    );
}
//设置相机
void SetCamera(vec2 uv,out vec3 ro, out vec3 rd){
  //步骤1 获得相机位置ro
  ro = vec3(0.0,5.0,5);//获取相机的位置 
  vec3 ta = vec3(0,0,10);//获取目标位置
  vec3 forward = normalize( ta - ro);//计算 forward 方向
  vec3 left = normalize(cross( vec3(0.0,1.0,0.0), forward ));//计算 left 方向
  vec3 up = normalize(cross(forward,left));////计算 up 方向
  const float zoom = 1.;

  //步骤2 获得射线朝向
  rd = normalize( uv.x*left + uv.y*up + zoom*forward );
}

//获取灯光
float GetLight(vec3 p)
{
    vec3 lightPos = vec3(0,12,10);
    lightPos.xz += vec2(sin(u_time),cos(u_time))*2.0;
    
    vec3 l = normalize(lightPos-p);//光方向
    vec3 n = calcNormal(p);
    
    float dif = dot(n,l);//漫反射颜色
    //计算阴影
	 float shadow = SoftShadow(p,l);
    return dif *shadow;
}

//射线检测
//ro:位置, rd:方向
float RayCast(vec3 ro, vec3 rd)
{
    float depth = 0.;//深度

    for(int i = 0; i < MAX_RAYCAST_STEPS; i++)
    {
        //对UV进行偏移，偏移方向为rd
        vec3 p = ro + rd * depth;
        //最短距离
        float ds = sceneSDF(p);
        depth += ds;
        if(depth>Max_Dist || ds < Surf_Dist) 
            break;
    }
                     
    return depth;     
}

void main() {
    vec2 uv = (2.0*gl_FragCoord.xy - u_resolution.xy)/u_resolution.y;
    
    vec3 color = vec3(0.);
    vec3 ro,rd;
   //设置Camera
    SetCamera(uv,ro,rd);
    //求射线和场景中物体的碰撞点p  
    float ret = RayCast(ro,rd);
    vec3 pos = ro+ret*rd;
 	 
    float dif = GetLight(pos);
    
    // vec4 sph = vec4( cos( u_time + vec3(2.0,1.0,1.0) + 0.0 )*vec3(1.5,0.0,1.0), 1.0 );sph.x = 1.0;
    // float shadow = sphSoftShadow(pos,rd,sph,7.);
    
    color = vec3(dif) ;
    
    gl_FragColor = vec4(color,1.);
}
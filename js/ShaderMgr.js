
window.ShaderMgr = (function () {
    var container;
    var camera, scene, renderer;
    var uniforms = {};
    var vertexShaderPath, fragmentShaderPath;
    var vertexShaderProgram, fragmentShaderProgram;
    var _update = ()=>{};
    console.log("12121");

    // 初始化
    function init(containerId) {
        // 读取文件
        readFile(vertexShaderPath, (vertexString) => {
            readFile(fragmentShaderPath, (fragmentString) => {
                vertexShaderProgram = vertexString;
                fragmentShaderProgram = fragmentString;
                initShader(containerId);
                animate();
            })
        })
    }

    // 设置shader程序
    function setShaderProgram(vertexShader, fragmentShader) {
        vertexShaderPath = vertexShader;
        fragmentShaderPath = fragmentShader;
    }

    // 设置uniforms 变量
    function setUniforms(name, type, value) {
        uniforms[name] = { type, value };
    }

    // 读取文件
    function readFile(path, callBack = () => { }) {
        $.ajax({
            url: path,
            success: function (data, status) {
                callBack(data)
            },
            error: function (data, status) {
                console.error(`读取${path}失败`)
            }
        });
    }

    // 初始化shader
    function initShader(containerId) {
        container = document.getElementById(containerId);
        // 摄像机
        camera = new THREE.Camera();
        camera.position.z = 1;
        // 场景
        scene = new THREE.Scene();
        // 创建一个平面
        var geometry = new THREE.PlaneBufferGeometry(2, 2);

        console.log("vertexShader:", vertexShaderPath);
        console.log("fragmentShader:", fragmentShaderPath);
        console.log("uniforms:", uniforms);
        //屏幕大小
        setUniforms("u_resolution","v2", new THREE.Vector2());
        // 运行时间
        setUniforms("u_time","f", 1.0)

        // 材质
        var material = new THREE.ShaderMaterial({
            uniforms: uniforms,
            vertexShader: vertexShaderProgram,
            fragmentShader: fragmentShaderProgram,
        });
        //
        var mesh = new THREE.Mesh(geometry, material);
        scene.add(mesh);

        renderer = new THREE.WebGLRenderer();
        renderer.setPixelRatio(window.devicePixelRatio);

        container.appendChild(renderer.domElement);
        //重新调整窗体大小
        onWindowResize();
        window.addEventListener('resize', onWindowResize, false);
    }

    // 窗口大小改变会触发
    function onWindowResize(event) {
        renderer.setSize(window.innerWidth, window.innerHeight);
        uniforms.u_resolution.value.x = renderer.domElement.width;
        uniforms.u_resolution.value.y = renderer.domElement.height;
    }

    function animate() {
        requestAnimationFrame(animate);
        render();
    }

    // 渲染
    function render() {
        uniforms.u_time.value += 0.05;
        _update();
        renderer.render(scene, camera);
    }

    function update(func) {
        _update = func;
    }

    return { init, setShaderProgram ,setUniforms,update}
}())

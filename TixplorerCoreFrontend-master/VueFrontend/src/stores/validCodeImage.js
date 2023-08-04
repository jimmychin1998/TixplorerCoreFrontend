var codetxt = {
    txtvalue:''
}

// 數字圖形驗證功能
function doFirst() {
    //存取canvas使用背景設計
    let options = {
        canvasId: "auth-code",//canvas標籤的id
        txt: (randomNum(100000, 999999)).toString(),//傳入驗證碼內容
        height: 46,//驗證碼的高度
        width: 130,//驗證碼的寬度
        fontColor1: 0,//隨機生成字型顏色
        fontColor2: 50,//隨機生成字型顏色
        bgColor1: 180,//這個範圍的顏色作背景看起來清晰一些
        bgColor2: 255,//這個範圍的顏色作背景看起來清晰一些
        fontStyle: "24px SimHei"
    }
    
    codetxt.txtvalue = options.txt

// 點擊「換圖按鈕時換圖」
var changeBtn = document.getElementById("changeImage");
changeBtn.addEventListener('click', () => {
    options.txt = (randomNum(100000, 999999)).toString();
    helper = new writeAuthCode(options);
    document.getElementById('errorText').innerHTML = "";
    document.getElementById('validText').value = "";
    codetxt.txtvalue = options.txt
});
// --------------------------------------------------
//圖形驗證碼建構
function writeAuthCode(options) {
    this.options = options;
    let canvas = document.getElementById(options.canvasId);
    canvas.width = options.width || 300
    canvas.height = options.height || 150
    //建立一個canvas物件
    let ctx = canvas.getContext('2d');
    ctx.textBaseline = "middle";
    ctx.fillStyle = randomColor(options.bgColor1, options.bgColor2);
    ctx.fillRect(0, 0, options.width, options.height);
    for (let i = 0; i < options.txt.length; i++) {
        //讓每個字不一樣
        let txt = options.txt.charAt(i);
        ctx.font = options.fontStyle;
        ctx.fillStyle = randomColor(options.fontColor1, options.fontColor2);
        ctx.shadowOffsetY = randomNum(-3, 3);
        ctx.shadowBlur = randomNum(-3, 3);
        ctx.shadowColor = "rgba(0, 0, 0, 0.3)";
        let x = options.width / (options.txt.length + 1) * (i + 1);
        let y = options.height / 2;
        let deg = randomNum(-30, 30);
        //設定旋轉角度和座標原點
        ctx.translate(x, y);
        ctx.rotate(deg * Math.PI / 180);
        ctx.fillText(txt, 0, 0);
        //恢復旋轉角度和座標原點
        ctx.rotate(-deg * Math.PI / 180);
        ctx.translate(-x, -y);
    }
    //1~4條隨機干擾線隨機出現
    for (let i = 0; i < randomNum(1, 4); i++) {
        ctx.strokeStyle = randomColor(40, 180);
        ctx.beginPath();
        ctx.moveTo(randomNum(0, options.width), randomNum(0, options.height));
        ctx.lineTo(randomNum(0, options.width), randomNum(0, options.height));
        ctx.stroke();
    }
    //繪製干擾點
    for (let i = 0; i < options.width / 6; i++) {
        ctx.fillStyle = randomColor(0, 255);
        ctx.beginPath();
        ctx.arc(randomNum(0, options.width), randomNum(0, options.height), 1, 0, 2 * Math.PI);
        ctx.fill();
    }
    this.validate = function (code) {
        return this.options.txt == code;
    }

};
//隨機數字
function randomNum(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}
//隨機顏色
function randomColor(min, max) {
    let r = randomNum(min, max);
    let g = randomNum(min, max);
    let b = randomNum(min, max);
    return "rgb(" + r + "," + g + "," + b + ")";
}

var helper = new writeAuthCode(options); 

}
// 數字圖形驗證功能結束

//畫面載入時執行function
window.addEventListener('load',doFirst)

//匯出驗證碼內容
export default codetxt
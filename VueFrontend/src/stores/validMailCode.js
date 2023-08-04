var validMailCode = {
    txtvalue:''
}

function doGenerateValidMailCode() {
    //亂數產生驗證碼
    let codeTxt = ''
    const RandomDatas = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z']
    for (let i = 0 ; i < 6 ; i++){
        //產生6位數驗證碼
        codeTxt += RandomDatas[randomNum(0,RandomDatas.length)]
    }
    validMailCode.txtvalue = codeTxt
}

//隨機數字
function randomNum(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

export default {
    doGenerateValidMailCode,
    validMailCode
}
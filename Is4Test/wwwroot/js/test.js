
console.log(document.cookie)
// init vConsole 
window.vConsole = new window.VConsole();
window.onerror = function (errorMessage, scriptURI, lineNumber, columnNumber, errorObj) {
    console.log("错误信息：", errorMessage);
    console.log("出错文件：", scriptURI);
    console.log("出错行号：", lineNumber);
    console.log("出错列号：", columnNumber);
    console.log("错误详情：", errorObj);
}
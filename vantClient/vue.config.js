const CompressionPlugin = require("compression-webpack-plugin")
module.exports = {
    devServer: {
        port: 8081, // 启动端口
        open: true, // 启动后是否自动打开网页,
        disableHostCheck: true
    },
    configureWebpack: config => {
        if (true) {
            return {
                plugins: [
                    new CompressionPlugin({
                        test: /\.js$|\.html$|.\css/, //匹配文件名
                        threshold: 10240,//对超过10k的数据压缩
                        deleteOriginalAssets: false //不删除源文件
                    })
                ]
            }
        }

    }
}
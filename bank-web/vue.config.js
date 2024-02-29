module.exports = {
    devServer: {
      proxy: {
        '/api': {
          target: 'http://localhost:7284',  // ปรับเปลี่ยน URL ของ .NET Core ที่ต้องการเรียก
          changeOrigin: true
        }
      }
    }
  }
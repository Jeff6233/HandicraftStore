//index.js
//获取应用实例
const app = getApp()

Page({
  data: {
    product:[]
  },
  //事件处理函数
  bindViewTap: function() {
    wx.navigateTo({
      url: '../logs/logs'
    })
  },
  onLoad: function () {
    var that = this;
    wx.request({
      url: 'http://localhost/Handicraft/api/Product/GetProduct',
      success(data) {
        that.setData({
          product: data.data.List
        })
        console.log(that.data.product)
      }
    })
  },
  openDetail:function(e){
    wx.request({
      url: `http://localhost/Handicraft/api/Product/GetProductByType?type=${e.currentTarget.dataset.type}`,
      success(data){
        wx.navigateTo({
          url: '../src/product/product',
          success: function (res) {
            // 通过eventChannel向被打开页面传送数据
            res.eventChannel.emit('product', { product: data.data })
          }
        })
      }
    })
  }
})

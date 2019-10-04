// pages/src/product/detail/detail.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    background: ['demo-text-1', 'demo-text-2', 'demo-text-3'],
    indicatorDots: true,
    vertical: false,//纵向
    autoplay: false,//自动
    circular: true,//衔接
    interval: 2000,//时长
    duration: 500,//自时长
    previousMargin: 0,//前边距
    nextMargin: 0,//后边距,
    bookclassify: [],
    bannerImage:[],
    product:[],
    userId:null
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that=this;
    wx.request({
      url: `http://localhost/Handicraft/api/Product/GetSingleProduct?productId=${options.id}`,
      success(data){
        that.setData({
          bannerImage:data.data[0].BannerImage,
          product:data.data[0]
        })
        console.log(that.data.product)
      }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function (res) {
    var that=this;
    console.log(that.data.product.Name)
    console.log(res)
    return {
      title: that.data.product.Name,
      path: 'pages/src/product/detail/detail'
      }
    },
  
  call:function(){
    wx.makePhoneCall({
      phoneNumber: '15906506583'
    })
  },
  goHome:function(){
    wx.switchTab({
      url: '../../../index/index',
    })
  },
  goShopingCar:function(){
    wx.switchTab({
      url: '../../../shopping/shopping',
    })
  },
  addShoppingCar:function(){
    var that=this;
    wx.getStorage({
      key: 'userId',
      success: function(res) {
        wx.request({
          url: 'http://localhost/Handicraft/api/ShoppingCar/AddShoppingCar',
          method: 'POST',
          data: {
            UserId: res.data,
            ProductId: that.data.product.Id,
            Count: 1,
          },
          success(res){
            wx.showToast({
              title: '添加成功',
              duration: 600
            })
          }
        })
      },
    })
    
    
  },
  goPayment:function(){
    wx.navigateTo({
      url: '../../../shopping/order_detail/order_detail',
    })
  }
})
// pages/shopping.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    shoppingCarList:[],
    isCheckedAll:false,
    productIds:[],
    totalAmount:'',
    block:true,
    paymentColor:'rgba(107, 103, 103, 0.897)'
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that=this;
    wx.getStorage({
      key: 'userId',
      success: function(res) {
        wx.request({
          url: `http://localhost/Handicraft/api/ShoppingCar/GetShoppingCarList?userId=${res.data}`,
          method:'GET',
          success(res){
            that.setData({
              shoppingCarList:res.data.List
            })
            console.log(that.data.shoppingCarList)
          }
        })
      },
    })
  },
  goShopping:function(){
    wx.switchTab({
      url: '../index/index',
    })
  },
  seletedAll:function(){
    var that=this;
    var isCheckedAll = that.data.isCheckedAll;
    if (!isCheckedAll){
      that.setData({
        isCheckedAll:true
      })
      that.data.shoppingCarList.forEach(function(i,k){
        that.data.productIds.push(i.Product.Id)
      })

      wx.getStorage({
        key: 'userId',
        success: function (res) {
          wx.request({
            url: `http://localhost/Handicraft/api/ShoppingCar/GetShoppingCarTotalAmount?ids=${that.data.productIds}&userId=${res.data}`,
            method: 'GET',
            success(res) {
              that.setData({
                totalAmount: res.data.totalAmount,
                block: false,
                paymentColor: 'red'
              })
            }
          })
        },
      })
      console.log(that.data.totalAmount)

    } else if (isCheckedAll){
      that.setData({
        isCheckedAll:false,
        totalAmount:'',
        block: true,
        paymentColor:'rgba(107, 103, 103, 0.897)'
      })
      that.data.productIds=[];
    }
  },
  selectItem:function(e){
    var that=this;
    that.data.productIds=[];
    that.data.productIds=e.detail.value;
    if(that.data.productIds.length>0){
      that.setData({
        block: false,
        paymentColor: 'red'
      })
      console.log(that.data.block)
    } else if (that.data.productIds.length == 0){
      that.setData({
        block:true,
        paymentColor: 'rgba(107, 103, 103, 0.897)'
      })
      console.log(that.data.block)
    }
    wx.getStorage({
      key: 'userId',
      success: function (res) {
        wx.request({
          url: `http://localhost/Handicraft/api/ShoppingCar/GetShoppingCarTotalAmount?ids=${that.data.productIds}&userId=${res.data}`,
          method: 'GET',
          success(res) {
            that.setData({
              totalAmount:res.data.totalAmount,
            })
          }
        })

      },
    })
  },
  goPayment:function(){
    var that=this;
    if(!that.data.block){
      wx.showToast({
        title: '已开放',
      })
    }
  },
  removeProduct:function(e){
    var productId = e.currentTarget.dataset.productid;
    var that=this;
    console.log(productId)
    wx.showModal({
      title: '消息',
      content: '确认删除此商品',
      showCancel:true,
      success(res){
        if(res.confirm){

          wx.getStorage({
            key: 'userId',
            success: function(res) {
              wx.request({
                url: `http://localhost/Handicraft/api/ShoppingCar/RemoveProductInShoppingCar?productId=${productId}&userId=${res.data}`,
                success(res){
                  wx.showToast({
                    title: '删除成功',
                    duration: 600
                  })
                  that.onLoad();
                }
              })
            },
          })
          
        }
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
    this.onLoad();
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
  onShareAppMessage: function () {

  }
})
function getTotalAmount(productIds, userId){
  var that=this;
  var totalAmount=0;
  wx.getStorage({
    key: 'userId',
    success: function(res) {
      wx.request({
        url: `http://localhost/Handicraft/api/ShoppingCar/GetShoppingCarTotalAmount?ids=${productIds}&userId=${res.data}`,
        method:'GET',
        success(res){
          totalAmount=res.data.totalAmount
        }
      })
    },
  })
  console.log(totalAmount)
  return totalAmount;
}
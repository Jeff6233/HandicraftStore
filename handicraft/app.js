//app.js
const util=require('./utils/util.js')
App({
  onLaunch: function () {
    // 展示本地存储能力
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)

    // 登录
    wx.login({
      success: res => {
        // 发送 res.code 到后台换取 openId, sessionKey, unionId
        wx.request({
          url: `https://api.weixin.qq.com/sns/jscode2session?appid=wx37e4f96499ed8a79&secret=584e474af98433f628c0c30dd4622ef0&js_code=${res.code}&grant_type=authorization_code`,
          data:{
            code:res.code
          },
          success(data){
            wx.setStorage({
              key: 'openid',
              data: data.data.openid,
            })
            wx.request({
              url: 'http://localhost/Handicraft/api/UserInfo/AddUserInfo',
              method:'POST',
              data:{
                OpenId: data.data.openid
              }
            })
            //发布
            wx.request({
              url: `http://localhost/Handicraft/api/UserInfo/GetSingleUserInfo?openId=${data.data.openid}`,
              method:'GET',
              success(data){
                wx.setStorage({
                  key: 'userId',
                  data: data.data.Id,
                })
                wx.setStorage({
                  key: 'userInfo',
                  data: data.data,
                })
              }
            })

          }
        })
      }
    })
    
    // 获取用户信息
    wx.getSetting({
      success: res => {
        if (res.authSetting['scope.userInfo']) {
          // 已经授权，可以直接调用 getUserInfo 获取头像昵称，不会弹框
          wx.getUserInfo({
            success: res => {
              // 可以将 res 发送给后台解码出 unionId
              this.globalData.userInfo = res.userInfo
              // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
              // 所以此处加入 callback 以防止这种情况
              if (this.userInfoReadyCallback) {
                this.userInfoReadyCallback(res)
              }
            }
          })
        }
      }
    })
  },
  globalData: {
    userInfo: null
  }
})
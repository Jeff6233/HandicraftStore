const md5=require('./md5.js')

const formatTime = date => {
  const year = date.getFullYear()
  const month = date.getMonth() + 1
  const day = date.getDate()
  const hour = date.getHours()
  const minute = date.getMinutes()
  const second = date.getSeconds()

  return [year, month, day].map(formatNumber).join('/') + ' ' + [hour, minute, second].map(formatNumber).join(':')
}

const formatNumber = n => {
  n = n.toString()
  return n[1] ? n : '0' + n
}

/**
 * 随机函数的产生
 */
function createNonceStr() {
  return Math.random().toString(36).substr(2, 15)
}
/**
 * 时间戳产生的函数
 */
function createTimeStamp() {
  return parseInt(new Date().getTime() / 1000) + ''
}

/**
 * MD5加密
 * @params str:加密字符串
 */
function createMd5(str){
  return md5.hex_md5(str.toString());
}

module.exports = {
  formatTime: formatTime,
  createTimeStamp:createTimeStamp,
  createNonceStr:createNonceStr,
  createMd5: createMd5
}

'use strict'

class ServiceResponse {

  constructor(code, success, message, data) {
    this.code = code
    this.success = success
    this.message = message
    this.data = data
  }

}

class SuccessServiceResponse extends ServiceResponse {

  constructor(message, data) {
    super(200, true, message, data)
  }

}

module.exports = { ServiceResponse, SuccessServiceResponse }

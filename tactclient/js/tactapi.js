/**
 * Tact API Client
 * @constructor
 * @param {String} API Key
 * @param {String} API Address
 * @param {Number} API Port
 */
var Tact = function(apiKey, host, port) {
  this.apiKey = apiKey;
  this.host = host || 'localhost';
  this.port = port || '58663';
  this.endPoint = '/Kontaktid.asmx/';
  
  this.authenticated = false;
};


/**
 * Lowlevel JSONP request
 * @private
 * 
 */
Tact.prototype.request = function(method, params, callback) {
  for(var i in params) {
    if(typeof params[i] == 'string') {
      params[i] = '"' + params[i] + '"';
    }
  }
  
  $.ajax({ 
    url: 'http://' + this.host + ':' + this.port 
      + this.endPoint + method,
    data: params,
    dataType: 'jsonp',
    success: function(json) {
      callback && callback(json.d);
    }
  });
};

Tact.prototype.login = function(params, cb) {
  this.request('LogiSisse', {
    kasutajanimi: params.kasutajanimi,
    parool: params.parool,
    voti: this.apiKey
  }, function(obj) {
    if(obj.Tyyp == 'OK') {
      this.authenticated = true;
    }
    cb && cb(obj);
  });
};

Tact.prototype.logout = function(cb) {
  this.request('LogiValja', {
  }, function(obj) {
    if(obj.Tyyp == 'OK') {
      this.authenticated = false;
    }
    cb && cb(obj);
  });
};

Tact.prototype.getUserDetails = function(cb) {
  this.request('KuvaKasutaja', {
  }, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.editUserDetails = function(params, cb) {
  var params = {
    parool: params.parool || '',
    eesnimi: params.eesnimi || '',
    perenimi: params.perenimi || ''
  };
  this.request('MuudaKasutaja', params, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.createUser = function(params, cb) {
  var params = {
    kasutajanimi: params.kasutajanimi || '',
    facebookId: params.facebookId || '',
    parool: params.parool || '',
    eesnimi: params.eesnimi || '',
    perenimi: params.perenimi || ''
  };
  this.request('LooKasutaja', params, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.createContact = function(params, cb) {
  var params = {
    perenimi: params.perenimi || '',
    eesnimi: params.eesnimi || '',
    telefonKodu: params.telefonKodu || '',
    telefonToo: params.telefonToo || '',
    telefonMob: params.telefonMob || '',
    emailKodu: params.emailKodu || '',
    emailToo: params.emailToo || '',
    riik: params.riik || '',
    maakond: params.maakond || '',
    asula: params.asula || '',
    tanav: params.tanav || '',
    maja_nr: params.maja_nr || '',
    wlm: params.wlm || '',
    facebook: params.facebook || '',
    orkut: params.orkut || '',
    skype: params.skype || '',
    twitter: params.twitter || '',
    pilt: params.pilt || ''
  };
  this.request('LisaKontakt', params, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.deleteContact = function(contactId, cb) {
  this.request('EemaldaKontakt', { 
    kontakt_id: contactId || ''
  }, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.viewContact = function(params, cb) {
  var params = {
    kontakt_id: params.kontakt_id || '',
    perenimi: params.perenimi || '',
    eesnimi: params.eesnimi || '',
    telefonKodu: params.telefonKodu || '',
    telefonToo: params.telefonToo || '',
    telefonMob: params.telefonMob || '',
    emailKodu: params.emailKodu || '',
    emailToo: params.emailToo || '',
    riik: params.riik || '',
    maakond: params.maakond || '',
    asula: params.asula || '',
    tanav: params.tanav || '',
    maja_nr: params.maja_nr || '',
    wlm: params.wlm || '',
    facebook: params.facebook || '',
    orkut: params.orkut || '',
    skype: params.skype || '',
    twitter: params.twitter || '',
    pilt: params.pilt || ''
  };
  this.request('KuvaKontakt', params, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.searchContact = function(param, cb) {
  this.request('OtsiKontakt', { param: param }, function(obj) {
    cb && cb(obj);
  });
};

Tact.prototype.editContact = function(contactId, params, cb) {
  var params = {
    kontakt_id: contactId || '',
    eesnimi: params.eesnimi || '',
    perenimi: params.perenimi || '',
    telefonKodu: params.telefonKodu || '',
    telefonToo: params.telefonToo || '',
    telefonMob: params.telefonMob || '',
    emailKodu: params.emailKodu || '',
    emailToo: params.emailToo || '',
    riik: params.riik || '',
    maakond: params.maakond || '',
    asula: params.asula || '',
    tanav: params.tanav || '',
    maja_nr: params.maja_nr || '',
    wlm: params.wlm || '',
    facebook: params.facebook || '',
    orkut: params.orkut || '',
    skype: params.skype || '',
    twitter: params.twitter || '',
    pilt: params.pilt || ''
  };
  this.request('MuudaKontakt', params, function(obj) {
    cb && cb(obj);
  });
};
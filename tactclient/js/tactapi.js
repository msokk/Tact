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
 * @param {String} Method name
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
Tact.prototype.request = function(method, params, callback) {
  for(var i in params) {
    if(typeof params[i] == 'string') {
      params[i] = '"' + escape(params[i]) + '"';
    }
  }
  
  $.ajax({ 
    url: 'http://' + this.host + ':' + this.port 
      + this.endPoint + method,
    data: params,
    dataType: 'jsonp',
    success: function(json) {
      var result = JSON.parse(unescape(JSON.stringify(json)));
      callback && callback(result.d);
    }
  });
};

/**
 * Tact Login
 * @public
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
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

/**
 * Tact logout
 * @public
 * @param {Function} Callback with result
 */
Tact.prototype.logout = function(cb) {
  this.request('LogiValja', {
  }, function(obj) {
    if(obj.Tyyp == 'OK') {
      this.authenticated = false;
    }
    cb && cb(obj);
  });
};

/**
 * Fetch authenticated user details
 * @public
 * @param {Function} Callback with result
 */
Tact.prototype.getUserDetails = function(cb) {
  this.request('KuvaKasutaja', {
  }, function(obj) {
    cb && cb(obj);
  });
};

/**
 * Edit authenticated user details
 * @public
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
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

/**
 * Create new user
 * @public
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
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

/**
 * Create new contact
 * @public
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
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

/**
 * Delete contact
 * @public
 * @param {Number} Contact ID
 * @param {Function} Callback with result
 */
Tact.prototype.deleteContact = function(contactId, cb) {
  this.request('EemaldaKontakt', { 
    kontakt_id: contactId || ''
  }, function(obj) {
    cb && cb(obj);
  });
};

/**
 * View a contact
 * @public
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
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

/**
 * Do search over contacts
 * @public
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
Tact.prototype.searchContact = function(param, cb) {
  this.request('OtsiKontakt', { param: param }, function(obj) {
    cb && cb(obj);
  });
};

/**
 * Edits contact
 * @public
 * @param {Number} Contact ID
 * @param {Object} Parameter dictionary
 * @param {Function} Callback with result
 */
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
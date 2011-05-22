window.TactClient = {};

TactClient.editContact = function(id) {

  var c = JSON.parse(window.localStorage.getItem('currentContacts'))[id];
  
  var result = '';
  result += '<div class="contact-header">';
  result += '<span class="profile" style="background: url(' + c.Pilt + ');"></span>';
  result +=   '<span class="nimi">' + c.Eesnimi + ' ' + c.Perenimi +  '</span>';
  result +=   '<span class="arrow">▼</span>';
  result += '</div>';
  result += '<div class="contact-body" style="display: block;">';
  result += '<table>';
  result += '<tr>'
  result +=   '<td>Eesnimi</td>';
  result +=   '<td class="eesnimi"><input type="text" value="' + c.Eesnimi + '" /></td>';
  result +=   '<td>Perenimi</td>';
  result +=   '<td class="perenimi"><input type="text" value="' + c.Perenimi + '" /></td>';
  result += '</tr>';
  result += '<tr>'
  result +=   '<td>Kodutelefon</td>';
  result +=   '<td class="telefonKodu"><input type="text" value="' + c.TelefonKodu + '" /></td>';
  result +=   '<td>Töötelefon</td>';
  result +=   '<td class="telefonToo"><input type="text" value="' + c.TelefonToo + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>Mobiil</td>';
  result +=   '<td class="telefonMob"><input type="text" value="' + c.TelefonMob + '" /></td>';
  result +=   '<td>E-Mail Kodu</td>';
  result +=   '<td class="emailKodu"><input type="text" value="' + c.EmailKodu + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>E-Mail Töö</td>';
  result +=   '<td class="emailToo"><input type="text" value="' + c.EmailToo + '" /></td>';
  result +=   '<td>Facebook</td>';
  result +=   '<td class="facebook"><input type="text" value="' + c.Facebook + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>WLM</td>';
  result +=   '<td class="wlm"><input type="text" value="' + c.WindowsLiveMessenger + '" /></td>';
  result +=   '<td>Twitter</td>';
  result +=   '<td class="twitter"><input type="text" value="' + c.Twitter + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>Orkut</td>';
  result +=   '<td class="orkut"><input type="text" value="' + c.Orkut + '" /></td>';
  result +=   '<td>Skype</td>';
  result +=   '<td class="skype"><input type="text" value="' + c.Skype + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>Riik</td>';
  result +=   '<td class="riik"><input type="text" value="' + c.Riik + '" /></td>';
  result +=   '<td>Maakond</td>';
  result +=   '<td class="maakond"><input type="text" value="' + c.Maakond + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>Asula</td>';
  result +=   '<td class="asula"><input type="text" value="' + c.Asula + '" /></td>';
  result +=   '<td>Tänav</td>';
  result +=   '<td class="tanav"><input type="text" value="' + c.Tanav + '" /></td>';
  result += '</tr>';
  result += '<tr>';
  result +=   '<td>Maja Nr</td>';
  result +=   '<td class="maja_nr"><input type="text" value="' + c.MajaNr + '" /></td>';
  result +=   '<td>Pilt</td>';
  result +=   '<td class="pilt"><input type="text" value="' + c.Pilt + '" /></td>';
  result += '</tr>';
  result += '</table>';
  result += '<span class="controls">';
  result +=   '<button onclick="TactClient.saveContact(' + c.Id + ')">Salvesta</button>';
  result +=   '<button onclick="TactClient.renderContact(null, ' + c.Id + ')">Katkesta</button>';
  result += '</span>';
  result += '<div class="clear"></div>';
  result += '</div>';
  
  $('#contact-' + id).html(result);  
};

TactClient.saveContact = function(id) {
  var params = {};
  $("input[type='text']").each(function(index, item) {
    params[$(item).parent().attr('class')] = $(item).val();
  });
  
  TactClient.Api.editContact(id, params, function(result) {
    var error = false;
    if(result.Tyyp == 'Viga') {
      error = true;
    } else {
      TactClient.Api.viewContact({
        kontakt_id: id
      }, function(contacts) {
        var cache = JSON.parse(window.localStorage.getItem('currentContacts'));
        cache[id] = contacts[0];
        window.localStorage.setItem('currentContacts', JSON.stringify(cache));
        TactClient.renderContact(null, id);
      });  
    }
    TactClient.notify(result.Sonum, error);
    TactClient.bindContactHandlers();
  });
};

TactClient.deleteContact = function(id) {
  var answer = confirm("Oled kindel?");
  if(answer) {
    TactClient.Api.deleteContact(id, function(result) {
      if(result.Tyyp == 'OK') {
        $('#contact-' + id).remove();
      } else {
        TactClient.notify(result.Sonum, error);
      }
    });
  }
};


//TODO: Refractor this and upper piece of *!
TactClient.renderContact = function(c, id) {
  if(id) {
    c = JSON.parse(window.localStorage.getItem('currentContacts'))[id];
  }

  var result = '';
  result += '<div class="contact-header">';
  result += (c.Pilt)? '<span class="profile" style="background: url(' + c.Pilt + ');"></span>' : '';
  result +=   '<span class="nimi">' + c.Eesnimi + ' ' + c.Perenimi +  '</span>';
  result +=   '<span class="arrow">►</span>';
  result += '</div>';
  result += '<div class="contact-body">';
  result += '<table>';
  result +=   '<tr>'
  result += (c.TelefonKodu)? '<td>Kodutelefon</td>': '';
  result += (c.TelefonKodu)? '<td class="homephone">' + c.TelefonKodu + '</td>': '';
  result += (c.TelefonToo)? '<td>Töötelefon</td>': '';
  result += (c.TelefonToo)? '<td class="workphone">' + c.TelefonToo + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.TelefonMob)? '<td>Mobiil</td>': '';
  result += (c.TelefonMob)? '<td class="mobile">' + c.TelefonMob + '</td>': '';
  result += (c.EmailKodu)? '<td>E-Mail Kodu</td>': '';
  result += (c.EmailKodu)? '<td class="homemail">' + c.EmailKodu + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.EmailToo)? '<td>E-Mail Töö</td>': '';
  result += (c.EmailToo)? '<td class="workmail">' + c.EmailToo + '</td>': '';
  result += (c.Facebook)? '<td>Facebook</td>': '';
  result += (c.Facebook)? '<td class="fb">' + c.Facebook + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.WindowsLiveMessenger)? '<td>WLM</td>': '';
  result += (c.WindowsLiveMessenger)? '<td class="wlm">' + c.WindowsLiveMessenger + '</td>': '';
  result += (c.Twitter)? '<td>Twitter</td>': '';
  result += (c.Twitter)? '<td class="twitter">' + c.Twitter + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.Orkut)? '<td>Orkut</td>': '';
  result += (c.Orkut)? '<td class="orkut">' + c.Orkut + '</td>': '';
  result += (c.Skype)? '<td>Skype</td>': '';
  result += (c.Skype)? '<td class="skype">' + c.Skype + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.Riik)? '<td>Riik</td>': '';
  result += (c.Riik)? '<td class="country">' + c.Riik + '</td>': '';
  result += (c.Maakond)? '<td>Maakond</td>': '';
  result += (c.Maakond)? '<td class="county">' + c.Maakond + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.Asula)? '<td>Asula</td>': '';
  result += (c.Asula)? '<td class="city">' + c.Asula + '</td>': '';
  result += (c.Tanav)? '<td>Tänav</td>': '';
  result += (c.Tanav)? '<td class="street">' + c.Tanav + '</td>': '';
  result +=   '</tr>';
  result +=   '<tr>';
  result += (c.MajaNr)? '<td>Maja Nr</td>': '';
  result += (c.MajaNr)? '<td class="housenr">' + c.MajaNr + '</td>': '';
  result +=   '</tr>';
  result +=   '</table>';
  result +=   '<span class="controls">';
  result +=   '<button onclick="TactClient.editContact(' + c.Id + ')">Muuda</button>';
  result +=   '<button onclick="TactClient.deleteContact(' + c.Id + ')">Kustuta</button>';
  result +=   '</span>';
  result +=   '<div class="clear"></div>';
  result += '</div>';

  if(id) {
    $('#contact-' + id).html(result);
  }
  
  return result;
}

TactClient.renderContacts = function(contacts) {
  /*if(!contacts) {
    location.href = '/index.html';
  }*/

  var result = '';
  var lsContacts = {};
  for(var i = 0; i < contacts.length; i++) {
    lsContacts[contacts[i].Id] = contacts[i];
    result += '<li id="contact-' + contacts[i].Id + '">';
    result += TactClient.renderContact(contacts[i]);
    result += '</li>';
  }
  window.localStorage.setItem('currentContacts', JSON.stringify(lsContacts));
  window.localStorage.setItem('currentResult', result);
  window.localStorage.setItem('currentSearch', $('#contactSearch').val() || '');
  $('#contactList').html(result);
  TactClient.bindContactHandlers();
};

TactClient.bindContactHandlers = function() {
  $('.contact-header').live('click', function() {
    var body = $(this).parent().find('.contact-body');
    if(body.css('display') == 'none') {
      $(this).find('.arrow').html('▼');
    } else {
      $(this).find('.arrow').html('►');
    }
    $(body).toggle();
  });
  
  $('.contact-body .fb').live('click', function() {
    window.location.href = $(this).text();
  });
  
  $('.contact-header span.profile').live('hover', function() {
    $(this).toggleClass('profile-fixed');
  }, function() {
    $(this).toggleClass('profile-fixed');
  });
};

TactClient.routes = {
  'login.html': function() {
  
    //Login logic
    var logIn = function(user, pass) {
      $('#status').html('<img id="loader" src="images/loader.gif" />');
      $('#loader').css('visibility', 'visible');
      TactClient.Api.login({
        kasutajanimi: user,
        parool: pass
      }, function(result) {
        if(result.Tyyp != 'OK') {
          $('#status').html(result.Sonum);
        } else {
          window.localStorage.setItem('loggedin', true);
          TactClient.go('contacts.html');
        }
      });
    };
  
    //Login handlers
    $('#loginBtn').click(function() {
      logIn($('input[name=\'username\']').val(),
        $('input[name=\'password\']').val());
    });
    $('input[name=\'password\'], input[name=\'username\']').keypress(function(e) {
      if(e.charCode == 13) {        
        logIn($('input[name=\'username\']').val(),
          $('input[name=\'password\']').val());
      }
    });
  },
  
  'contacts.html': function() {
    var cached = window.localStorage.getItem('currentResult');
    var cachedSearch = window.localStorage.getItem('currentSearch') || '';
    $('#contactSearch').val(cachedSearch);
    
    if(cached) {
      $('#contactList').html(cached);
    }
    TactClient.Api.searchContact(cachedSearch, function(contacts) {
      TactClient.renderContacts(contacts);
    });  
    $('#hpLink').attr('onclick', 'TactClient.go(\'contacts.html\')');
    $('#footerLeft a').first().remove();
    $('#footerLeft span').remove();
    $('#footerLeft').prepend('<span><a href="javascript:void(0);" onclick='
      +'"TactClient.go(\'profile.html\')">Profiil</a> <img src="images/'
      +'menuSplitter.png" /> <a href="javascript:void(0);" onclick='
      +'"TactClient.logout()">Logi Välja</a> </span>');
    
    
    $('#contactSearch').keyup(function(e) {
      var value = $(this).val();
      clearTimeout(TactClient.timer);
      TactClient.timer = setTimeout(function() {
        TactClient.Api.searchContact(value, function(contacts) {
          TactClient.renderContacts(contacts);
        });        
      }, 100);
    });
  },
  
  'addcontact.html': function() {
    $('#saveContact').click(function() {
      var params = {};
      $("input[type='text']").each(function(index, item) {
        params[$(item).attr('name')] = $(item).val();
      });
      
      //TODO: Validation here!
      TactClient.Api.createContact(params, function(result) {
        var error = false;
        if(result.Tyyp == 'Viga') {
          error = true;
        } else {
          setTimeout(function() {
            History.back();
          }, 1500);
        }
        TactClient.notify(result.Sonum, error);

      });   
    });
  },
  
  'register.html': function() {

    $('#registerBtn').click(function() {
      var params = {};
      $("input[type='text']").each(function(index, item) {
        var value = $(item).val();
        var key = $(item).attr('name');
        params[key] = value;
        
      });
      params.parool = $("input[name='parool']").val();
      params.parool2 = $("input[name='parool2']").val();
      
      if(params.parool != '' && params.parool2 != '' && params.parool != params.parool2) {
        TactClient.notify('Paroolid ei kattu!', true);
        return;
      }
      
      TactClient.Api.createUser(params, function(result) {
          var error = false;
          if(result.Tyyp == 'Viga') {
            error = true;
          }
          TactClient.notify(result.Sonum, error);
      });
    });
  },
  
  'profile.html': function() {
    TactClient.Api.getUserDetails(function(details) {
      $("input[name='eesnimi']").val(details.Eesnimi);
      $("input[name='perenimi']").val(details.Perenimi);
    });
    
    $("input[name='eesnimi'], input[name='perenimi']").click(function() {
      $(this).select();
    });
    
    
    
    
    var getSession = function(cb) {
      FB.getLoginStatus(function(response) {
        if(response.session) {
          cb && cb(response.session);
        } else {
          FB.login(function(response) {
            if (response.session) {
              cb && cb(response.session);
            }
          }, {perms:'friends_about_me,friends_location'});
        }
      });
    };
    $('#profilesaveBtn').click(function() {
      var params = {
        eesnimi: $("input[name='eesnimi']").val(),
        perenimi: $("input[name='perenimi']").val()
      };
      if($("input[name='parool']").val() != '') {
        params.parool = $("input[name='parool']").val();
        params.parool2 = $("input[name='parool2']").val();
        
        if(params.parool != params.parool2) {
          TactClient.notify('Paroolid ei kattu!', true);
          return;
        }
      }
      
      TactClient.Api.editUserDetails(params, function(result) {
        var error = false;
        if(result.Tyyp == 'Viga') {
          error = true;
        } else {
          setTimeout(function() {
            History.back();
          }, 1500);
        }
        TactClient.notify(result.Sonum, error);
      });
    });
    
    
    
    $('.facebook').click(function() {
    
      getSession(function(session) {
        FB.api('/me/friends', function(response) {
          TactClient.notify('Impordin: 0 / ' + response.data.length);
          var progress = 0;
          for(var i = 0; i < response.data.length; i++) {
            FB.api('/' + response.data[i].id, function(user) {
              var location = ['',''];
              if(user.location && user.location.name) {
                location = user.location.name.split(', ');
              }
              TactClient.Api.createContact({
                eesnimi: user.first_name,
                perenimi: user.last_name,
                facebook: user.link,
                asula: location[0],
                riik: location[1],
                pilt: 'https://graph.facebook.com/' + user.id + '/picture?type=large'
              }, function(result) {
                progress++;
                TactClient.notify('Impordin: ' + progress + ' / ' + response.data.length);
              });
            });
          }
        });
      });
      
    });

  }

};
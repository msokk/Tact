window.TactClient = {};

TactClient.editContact = function(id) {

};

TactClient.deleteContact = function(id) {
  var answer = confirm("Oled kindel?");
  if(answer) {
    TactClient.Api.deleteContact(id, function(result) {
      if(result.Tyyp == 'OK') {
        $('#contact-' + id).remove();
      } else {
      
      }
      alert(result.Sonum);
    });
  }
};

//TODO: Refractor this piece of s***!
TactClient.renderContacts = function(contacts) {
  if(!contacts) {
    location.href = '/index.html';
  }

  var result = '';
  for(var i = 0; i < contacts.length; i++) {
    var c = contacts[i];
    result += '<li id="contact-' + c.Id + '">';
    result += '<div class="contact-header">';
    result += (c.Pilt)? '<img class="profile" src="' + c.Pilt + '">' : '';
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
    result += (c.EmailKodu)? '<td class="homemail">' + c.TelefonKodu + '</td>': '';
    result +=   '</tr>';
    result +=   '<tr>';
    result += (c.EmailToo)? '<td>E-Mail Töö</td>': '';
    result += (c.EmailToo)? '<td class="workmail">' + c.TelefonKodu + '</td>': '';
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
    result += '</li>';
  }

  $('#contactList').html(result);
  
  
  $('.contact-header').click(function() {
    var body = $(this).parent().find('.contact-body');
    if(body.css('display') == 'none') {
      $(this).find('.arrow').html('▼');
    } else {
      $(this).find('.arrow').html('►');
    }
    $(body).toggle();
  });
  
  $('.contact-header img').hover(function() {
    $(this).toggleClass('profile');
    $(this).toggleClass('profile-fixed');
  }, function() {
    $(this).toggleClass('profile');
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
    TactClient.Api.searchContact('', function(contacts) {
      TactClient.renderContacts(contacts);
    });  
    $('#hpLink').attr('onclick', 'TactClient.go(\'contacts.html\')');
    $('#footerLeft a').first().remove();
    $('#footerLeft span').remove();
    $('#footerLeft').prepend('<span><a href="javascript:void();" onclick='
      +'"TactClient.go(\'profile.html\')">Profiil</a> <img src="images/'
      +'menuSplitter.png" /> <a href="javascript:void();" onclick='
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
  }



};
window.TactClient = {};

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
          History.navigate('contacts.html');
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
    
    $('#hpLink').attr('onclick', 'History.navigate(\'contacts.html\')');
    $('#footerLeft a').first().remove();
    $('#footerLeft span').remove();
    $('#footerLeft').prepend('<span><a href="javascript:void();" onclick='
      +'"History.navigate(\'profile.html\')">Profiil</a> <img src="images/'
      +'menuSplitter.png" /> <a href="javascript:void();" onclick='
      +'"TactClient.logout()">Logi Välja</a> </span>');
    
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
  },
  
  'addcontact.html': function() {
    
  
  }



};
$(function() {
  //Create api client
  var api = TactClient.Api = new Tact(1234);
  //Setup History.JS
  var History = window.History;
  
  //Setup router
  History.Adapter.bind(window, 'statechange', function() {
    var state = History.getState();
    
    if(state.hash.indexOf('/' != -1)) {
      var frags = state.hash.split('/');
      state.hash = frags[frags.length-1];
    } else {
      state.hash = '/' + state.hash;
    }
    $.get(state.hash, function(data) {

        $('#main').html(data);
        if(TactClient.routes[state.hash]) {
          TactClient.routes[state.hash]();
        }
        
        if(window.firstUrl) {
          TactClient.go(window.firstUrl);
          delete window.firstUrl;
        }
    });
  });
  
  //Helper method
  TactClient.go = function(url) {
    History.pushState(null, null, url);
  };  
  
  TactClient.logout = function() {
    delete window.localStorage['loggedin'];
    TactClient.Api.logout(function() {
      window.location.href = '/index.html';
    });
  };
  
  //Fragment switch
  if(location.search != "") {
    window.firstUrl = location.search.substr(1);
  }
  
  //Switch page on login
  if(window.localStorage['loggedin']) {
    TactClient.Api.getUserDetails(function(data) { 
      if(data == null) {
        History.replaceState(null,null,'login.html');
      } else {
        TactClient.go('contacts.html');
      }
  
    });
  } else {
    History.replaceState(null,null,'login.html');
  }
});
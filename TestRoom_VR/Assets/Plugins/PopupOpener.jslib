var PopupOpenerPlugin = {
  PopupOpenerCaptureClick: function(number) {

    var code = number;
    var OpenPopup = function() {

      var urlString = "http://www.virtualspace.matters-of-activity.de/annualconference/?id=" + code;
        window.open(urlString, '_self');
        document.removeEventListener('keyup', OpenPopup);
    };
    document.addEventListener('keyup', OpenPopup);
  }
};
mergeInto(LibraryManager.library, PopupOpenerPlugin);

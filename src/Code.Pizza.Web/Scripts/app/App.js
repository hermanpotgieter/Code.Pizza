// this is an IIFE
// Immediately Invoked Function Expression
(function(app, $, undefined) {

    app.SayHello = function(name) {
        alert('Hello ' + name);
    };


})(App = {}, jQuery);

(function() {

    //App.SayHello('Herman');

})();

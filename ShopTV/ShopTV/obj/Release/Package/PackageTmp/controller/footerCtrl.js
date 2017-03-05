app.controller("footerCtrl", function ($scope, $http) {

    $http({method: 'GET', url: $scope.urlContacts + 'GetContacts'
    }).then(function mySucces(response) {
        $scope.contacts = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.contacts = response.statusText;
    });
});

$(window).scroll(function () {
    if ($(window).scrollTop() >= 200) {
        $('.w3-hover-deep-orange').fadeIn(500);
    } else {
        $('.w3-hover-deep-orange').fadeOut(500);
    }
});
function page_scroll2top() {
    $('html,body').animate({
        scrollTop: 0
    }, 800);
}
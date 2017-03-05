app.controller("newsCtrl", function ($scope, $http, $routeParams) {
    $scope.param5 = $routeParams.id;
    $scope.$emit('LOAD');

    $http({
        method: 'GET', url: $scope.urlNewsServices + 'GetNewsServices'
    }).then(function mySucces(response) {
        $scope.news = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.news = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlNewsServices + 'GetNewsService/' + $scope.param5
    }).then(function mySucces(response) {
        $scope.newsdetail = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.newsdetail = response.statusText;
    });

});
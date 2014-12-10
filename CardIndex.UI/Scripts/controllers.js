'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers', [])

    // Path: /
    .controller('HomeCtrl', ['$scope', '$location', '$window', '$modal', 'BookApi', function ($scope, $location, $window, $modal, bookApi) {
        $scope.$root.isLoading = false;
        $scope.books = bookApi.query();
        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /Genres
    .controller('GenreCtrl', ['$scope', '$location', '$window', '$modal', 'GenreApi', function ($scope, $location, $window, $modal, genreApi) {

        $scope.$root.isLoading = true;
        //get genres
        $scope.genres = genreApi.query({}, function () {
            $scope.$root.isLoading = false;
        });

        //modal dialog
        $scope.genreDialog = function (genre) {
            var modalInstance = $modal.open({
                templateUrl: '/views/templates/genrepartial',
                controller: GenrePartialCtrl,
                resolve: {
                    item: function () {
                        if (!genre) {
                            genre = { Id: 0, Name: '' };
                        };
                        return genre;
                    },
                }
            });

            modalInstance.result.then(function (item) {
                //if new item
                if (item.Id == 0) {
                    genreApi.save({}, JSON.stringify(item), function (data) {
                        item.Id = data.Id;
                        $scope.genres.push(item);
                    });
                }
                    //else update item
                else {
                    genreApi.update({}, JSON.stringify(item));
                }
            });
        };

        //confirmation modal dialog
        $scope.confirmationDialog = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/views/templates/confirmationpartial',
                controller: ConfirmationPartialCtrl,
                resolve: {
                    item: function () {
                        return data;
                    },
                }
            });
            //delete genre
            modalInstance.result.then(function (item) {
                genreApi.delete({ id: item.Id });
                _.remove($scope.genres, item);
            });
        };

        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

     // Path: /Authors
    .controller('AuthorCtrl', ['$scope', '$location', '$window', '$modal', 'AuthorApi', function ($scope, $location, $window, $modal, authorApi) {

        $scope.$root.isLoading = true;
        //get authors 
        $scope.authors = authorApi.query({}, function () {
            $scope.$root.isLoading = false;
        });

        //confirmation modal dialog
        $scope.confirmationDialog = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/views/templates/confirmationpartial',
                controller: ConfirmationPartialCtrl,
                resolve: {
                    item: function () {
                        return data;
                    },
                }
            });
            //delete author
            modalInstance.result.then(function (item) {
                authorApi.delete({ id: item.Id });
                _.remove($scope.authors, item);
            });
        };

        //author modal dialog
        $scope.authorDialog = function (author) {
            var modalInstance = $modal.open({
                templateUrl: '/views/templates/authorpartial',
                controller: AuthorPartialCtrl,
                resolve: {
                    item: function () {
                        if (!author) {
                            author = { Id: 0, Name: '' };
                        };
                        return author;
                    },
                }
            });

            modalInstance.result.then(function (item) {
                //if new item
                if (item.Id == 0) {
                    authorApi.save({}, JSON.stringify(item), function (data) {
                        item.Id = data.Id;
                        $scope.authors.push(item);
                    });
                }
                    //else update item
                else {
                    authorApi.update({}, JSON.stringify(item));
                }
            });
        };

        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /about
    .controller('AboutCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'AngularJS SPA | About';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /login
    .controller('LoginCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.isLoading = false;
        $scope.$root.title = 'AngularJS SPA | Sign In';
        // TODO: Authorize a user
        $scope.login = function () {
            $location.path('/');
            return false;
        };
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /error/404
    .controller('Error404Ctrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.isLoading = false;
        $scope.$root.title = 'Error 404: Page Not Found';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);

var GenrePartialCtrl = function ($scope, $modalInstance, item) {
    $scope.genre = item;
    $scope.ok = function (genre) {
        if ((!genre.Name == '')) {
            $scope.error = false;
            $modalInstance.close(genre);
        } else {
            $scope.error = true;
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

var AuthorPartialCtrl = function ($scope, $modalInstance, item) {
    $scope.author = item;
    $scope.ok = function (author) {
        if ((!author.Name == '')) {
            $scope.error = false;
            $modalInstance.close(author);
        } else {
            $scope.error = true;
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

var ConfirmationPartialCtrl = function ($scope, $modalInstance, item) {
    $scope.data = item;
    $scope.yes = function (data) {
        $modalInstance.close(data);
    };
    $scope.no = function () {
        $modalInstance.dismiss('cancel');
    };
}
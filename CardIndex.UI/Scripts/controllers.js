'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers', [])

    // Path: /
    .controller('HomeCtrl', ['$scope', '$location', '$window', '$modal', 'BookApi', 'GenreApi', 'AuthorApi', function ($scope, $location, $window, $modal, bookApi, genreApi, authorApi) {

        $scope.itemsPerPage = -1;
        $scope.currentPage = 1;
        $scope.$root.isLoading = true;

        //get books
        bookApi.get({ offset: ($scope.currentPage - 1) * $scope.itemsPerPage, limit: $scope.itemsPerPage },
                function (data) {
                    $scope.$root.isLoading = false;
                    $scope.books = data.books;
                    $scope.totalItems = data.count;
                    $scope.itemsPerPage = data.itemsPerPage;
                });

        //modal dialog
        $scope.bookDialog = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/views/templates/bookpartial',
                controller: BookPartialCtrl,
                resolve: {
                    item: function () {
                        var resultData = {
                            book: data,
                            AuthorApi: authorApi,
                            GenreApi: genreApi
                        };
                        if (!data) {
                            resultData.book = { Id: 0, Name: '', Isbn: '', Etc: '', Authors: [], Genres: [] };
                        };
                        return resultData;
                    }
                }
            });

            modalInstance.result.then(function (item) {
                //if new item
                console.log($scope.books[0]);
                console.log(item);
                if (item.Id == 0) {
                    bookApi.save({}, JSON.stringify(item), function (resultData) {
                        if ($scope.books.length < $scope.itemsPerPage) {
                            item.Id = resultData.Id;
                            $scope.books.push(item);
                        }
                        $scope.totalItems++;
                    });
                }
                    //else update item
                else {
                    bookApi.update({}, JSON.stringify(item));
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
            //delete book
            modalInstance.result.then(function (item) {
                bookApi.delete({ id: item.Id });
                _.remove($scope.books, item);
                $scope.totalItems--;
            });
        };

        //pagination
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.isLoading = true;
            bookApi.get({ offset: ($scope.currentPage - 1) * $scope.itemsPerPage, limit: $scope.itemsPerPage },
                function (data) {
                    $scope.isLoading = false;
                    $scope.books = data.books;
                    $scope.totalItems = data.count;
                    $scope.itemsPerPage = data.itemsPerPage;
                });
        };

        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /Genres
    .controller('GenreCtrl', ['$scope', '$location', '$window', '$modal', 'GenreApi', function ($scope, $location, $window, $modal, genreApi) {

        $scope.itemsPerPage = -1;
        $scope.currentPage = 1;
        $scope.$root.isLoading = true;
        //get genres
        genreApi.get({ offset: ($scope.currentPage - 1) * $scope.itemsPerPage, limit: $scope.itemsPerPage }, function (data) {
            $scope.$root.isLoading = false;
            $scope.genres = data.genres;
            $scope.totalItems = data.count;
            $scope.itemsPerPage = data.itemsPerPage;
        });

        //pagination
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.isLoading = true;
            genreApi.get({ offset: ($scope.currentPage - 1) * $scope.itemsPerPage, limit: $scope.itemsPerPage },
                function (data) {
                    $scope.isLoading = false;
                    $scope.genres = data.genres;
                    $scope.totalItems = data.count;
                    $scope.itemsPerPage = data.itemsPerPage;
                });
        };

        //modal dialog
        $scope.genreDialog = function (genre) {
            var modalInstance = $modal.open({
                templateUrl: '/views/templates/genrepartial',
                controller: GenrePartialCtrl,
                resolve: {
                    item: function () {
                        if (!genre) {
                            genre = { GenreId: 0, Name: '' };
                        };
                        return genre;
                    },
                }
            });

            modalInstance.result.then(function (item) {
                //if new item
                if (item.GenreId == 0) {
                    genreApi.save({}, JSON.stringify(item), function (data) {

                        if ($scope.genres.length < $scope.itemsPerPage) {
                            item.GenreId = data.GenreId;
                            $scope.genres.push(item);
                        }
                        $scope.totalItems++;
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
                genreApi.delete({ id: item.GenreId });
                $scope.totalItems--;
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

        $scope.itemsPerPage = -1;
        $scope.currentPage = 1;
        $scope.$root.isLoading = true;

        //get authors 
        authorApi.get({ offset: ($scope.currentPage - 1) * $scope.itemsPerPage, limit: $scope.itemsPerPage }, function (data) {
            $scope.$root.isLoading = false;
            $scope.authors = data.authors;
            $scope.totalItems = data.count;
            $scope.itemsPerPage = data.itemsPerPage;
        });

        //pagination
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.isLoading = true;
            authorApi.get({ offset: ($scope.currentPage - 1) * $scope.itemsPerPage, limit: $scope.itemsPerPage },
                function (data) {
                    $scope.isLoading = false;
                    $scope.authors = data.authors;
                    $scope.totalItems = data.count;
                    $scope.itemsPerPage = data.itemsPerPage;
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
            //delete author
            modalInstance.result.then(function (item) {
                authorApi.delete({ id: item.AuthorId });
                _.remove($scope.authors, item);
                $scope.totalItems--;
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
                            author = { AuthorId: 0, Name: '' };
                        };
                        return author;
                    },
                }
            });

            modalInstance.result.then(function (item) {
                //if new item
                if (item.AuthorId == 0) {
                    authorApi.save({}, JSON.stringify(item), function (data) {
                        if ($scope.authors.length < $scope.itemsPerPage) {
                            item.AuthorId = data.AuthorId;
                            $scope.authors.push(item);
                        }
                        $scope.totalItems++;
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

var BookPartialCtrl = function ($scope, $modalInstance, item) {

    $scope.book = item.book;
    $scope.isDialogDataLoading = { Authors: true, Genres: true };
    $scope.AuthorCustomTexts = { buttonDefaultText: 'Select Authors' };
    $scope.GenreCustomTexts = { buttonDefaultText: 'Select Genres' };

    item.AuthorApi.get({}, function (data) {
        $scope.isDialogDataLoading.Authors = false;
        $scope.AllAuthors = data.authors;
    });

    item.GenreApi.get({}, function (data) {
        $scope.isDialogDataLoading.Genres = false;
        $scope.AllGenres = data.genres;
    });

    $scope.AuthorDropDownSettings = { displayProp: 'Name', idProp: 'AuthorId', externalIdProp: '' };
    $scope.GenreDropDownSettings = { displayProp: 'Name', idProp: 'GenreId', externalIdProp: '' };

    $scope.ok = function (book) {
        if ((!book.Name == '') && (!book.Isbn == '') && (!book.Etc == '')) {
            $scope.error = false;
            $modalInstance.close(book);
        } else {
            $scope.error = true;
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

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
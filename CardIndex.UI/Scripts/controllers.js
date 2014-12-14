'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers', [])

    // Path: /
    .controller('HomeCtrl', ['$scope', '$location', '$window', '$modal', 'BookApi', 'GenreApi', 'AuthorApi', function ($scope, $location, $window, $modal, bookApi, genreApi, authorApi) {

        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.$root.isLoading = true;

        //get books
        loadBooks();

        //modal dialog
        $scope.bookDialog = function (data) {
            var originalItem = angular.copy(data);
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
                        } else {
                            resultData.book = data;
                            resultData.book.Authors.forEach(function (element, index) {

                                if (element.hasOwnProperty('Author') == true) {
                                    console.log(element.hasOwnProperty('Author'));
                                    resultData.book.Authors[index] = { Id: element.Author.Id, Name: element.Author.Name };
                                } else {
                                    resultData.book.Authors[index] = element;
                                }
                            });
                            resultData.book.Genres.forEach(function (element, index) {
                                if (element.hasOwnProperty('Genre') == true) {
                                    resultData.book.Genres[index] = { Id: element.Genre.Id, Name: element.Genre.Name };
                                } else {
                                    resultData.book.Genres[index] = element;
                                }
                            });
                        }

                        return resultData;
                    }
                }
            });

            modalInstance.result.then(function (item) {
                var requestItem = angular.copy(item);

                requestItem.Authors.forEach(function (element, index) {
                    requestItem.Authors[index] = { AuthorId: element.Id };
                });

                requestItem.Genres.forEach(function (element, index) {
                    requestItem.Genres[index] = { GenreId: element.Id };
                });
                //if new item
                if (item.Id == 0) {

                    if ($scope.books.length < $scope.itemsPerPage) {
                        $scope.books.push(item);
                    }
                    bookApi.save({}, angular.toJson(requestItem), function (resultData) {
                        item.Id = resultData.Id;
                    });
                    $scope.totalItems++;
                }

                    //else update item
                else {
                    delete requestItem['Authors@odata.context'];
                    bookApi.update({ key: item.Id }, angular.toJson(requestItem));
                }
            }, function () {
                if (!data.Name) return;
                data.Name = originalItem.Name;
                data.Isbn = originalItem.Isbn;
                data.Etc = originalItem.Etc;
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
                bookApi.delete({ key: item.Id });
                _.remove($scope.books, item);
                $scope.totalItems--;
            });
        };

        //pagination
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.$root.isLoading = true;
            loadBooks();
        };

        //sorting
        $scope.sortParam = { column: '', direction: '' };
        $scope.sort = function (column) {
            $scope.sortParam.column = column;
            if ($scope.sortParam.direction == 'asc') {
                $scope.sortParam.direction = 'desc';

            }
            else if ($scope.sortParam.direction == 'desc') {
                $scope.sortParam.direction = 'asc';
            } else {

                $scope.sortParam.direction = 'asc';
            }
            loadBooks();

        };

        //filtering
        $scope.status = {
            isopen: false
        };
        $scope.filterTypes = ['None', 'Name', 'Isbn', 'Etc', 'Genres', 'Authors'];

        $scope.filterValue = '';
        $scope.filter = function (param) {
            if (param != 'None') {
                $scope.currentFilter = param;
                
            } else {
                $scope.currentFilter = '';
            }
            loadBooks();

        };

        function loadBooks() {
            var sortString = undefined;
            var filterString = undefined;
            if ($scope.sortParam !== undefined) {
                if ($scope.sortParam.column != '' && $scope.sortParam.direction != '') {
                    sortString = $scope.sortParam.column + ' ' + $scope.sortParam.direction;
                }
            }

            if ($scope.currentFilter) {
                filterString = $scope.currentFilter + ' eq ' + "'" + $scope.filterValue + "'";
                console.log(filterString);
            }

            bookApi.get({ $expand: 'Genres($expand=Genre),Authors($expand=Author)', $filter: filterString, $orderby: sortString, $count: true, $skip: ($scope.currentPage - 1) * $scope.itemsPerPage, $top: $scope.itemsPerPage },
                 function (data) {
                     $scope.$root.isLoading = false;
                     $scope.books = data.value;
                     $scope.totalItems = data["@odata.count"];
                 });
        }

        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /Genres
    .controller('GenreCtrl', ['$scope', '$location', '$window', '$modal', 'GenreApi', function ($scope, $location, $window, $modal, genreApi) {

        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.$root.isLoading = true;
        //get genres
        loadGenres();

        //pagination
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.$root.isLoading = true;
            loadGenres();
        };

        //modal dialog
        $scope.genreDialog = function (genre) {
            var originalItem = angular.copy(genre);
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
                    genreApi.save({}, angular.toJson(item), function (data) {
                        if ($scope.genres.length < $scope.itemsPerPage) {
                            item.Id = data.Id;
                            $scope.genres.push(item);
                        }
                        $scope.totalItems++;
                    });
                }
                    //else update item
                else {
                    genreApi.update({ key: item.Id }, angular.toJson(item));
                }
            }, function () {
                if (!genre.Name) return;
                genre.Name = originalItem.Name;
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
                genreApi.delete({ key: item.Id });
                $scope.totalItems--;
                _.remove($scope.genres, item);
            });
        };

        //load genres
        function loadGenres() {
            genreApi.get({ $count: true, $skip: ($scope.currentPage - 1) * $scope.itemsPerPage, $top: $scope.itemsPerPage }, function (data) {
                $scope.$root.isLoading = false;
                $scope.genres = data.value;
                $scope.totalItems = data["@odata.count"];
            });
        }

        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

     // Path: /Authors
    .controller('AuthorCtrl', ['$scope', '$location', '$window', '$modal', 'AuthorApi', function ($scope, $location, $window, $modal, authorApi) {

        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.$root.isLoading = true;

        //get authors 
        loadAuthors();

        //pagination
        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.$root.isLoading = true;
            loadAuthors();
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
                authorApi.delete({ key: item.Id });
                _.remove($scope.authors, item);
                $scope.totalItems--;
            });
        };

        //author modal dialog
        $scope.authorDialog = function (author) {
            var originalIntem = angular.copy(author);
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
                    authorApi.save({}, angular.toJson(item), function (data) {
                        if ($scope.authors.length < $scope.itemsPerPage) {
                            item.Id = data.Id;
                            $scope.authors.push(item);
                        }
                        $scope.totalItems++;
                    });
                }
                    //else update item
                else {
                    authorApi.update({ key: item.Id }, angular.toJson(item));
                }
            }, function () {
                if (!author.Name) return;
                author.Name = originalIntem.Name;
            });
        };

        function loadAuthors() {
            authorApi.get({ $count: true, $skip: ($scope.currentPage - 1) * $scope.itemsPerPage, $top: $scope.itemsPerPage }, function (data) {
                $scope.$root.isLoading = false;
                $scope.authors = data.value;
                $scope.totalItems = data["@odata.count"];
            });
        }

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

    var sAuthors = angular.copy(item.book.Authors);
    var sGenres = angular.copy(item.book.Genres);
    $scope.book = item.book;
    $scope.isDialogDataLoading = { Authors: true, Genres: true };
    $scope.AuthorCustomTexts = { buttonDefaultText: 'Select Authors' };
    $scope.GenreCustomTexts = { buttonDefaultText: 'Select Genres' };

    item.AuthorApi.get({}, function (data) {
        $scope.isDialogDataLoading.Authors = false;
        $scope.AllAuthors = data.value;
    });

    item.GenreApi.get({}, function (data) {
        $scope.isDialogDataLoading.Genres = false;
        $scope.AllGenres = data.value;
    });

    $scope.AuthorDropDownSettings = { displayProp: 'Name', idProp: 'Id', externalIdProp: '', enableSearch: true };
    $scope.GenreDropDownSettings = { displayProp: 'Name', idProp: 'Id', externalIdProp: '', enableSearch: true };

    $scope.ok = function (book) {
        if ((!book.Name == '') && (!book.Isbn == '') && (!book.Etc == '')) {
            $scope.error = false;
            $modalInstance.close(book);
        } else {
            $scope.error = true;
        }
    };
    $scope.cancel = function () {
        $scope.book.Authors = sAuthors;
        $scope.book.Genres = sGenres;
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
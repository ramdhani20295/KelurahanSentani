angular.module("app.service", [])
    .factory("BaseUrl", function () {
        var service = {};
        service.URL = "http://localhost:52814";
        return service;
    })

    .factory("Helpers", function ()
    {
        var service = {};
        var messages = [];
        Load();

        service.getMessage = function (code,message)
        {
           angular.forEach(messages, function (value, key) {
               if (value.code == code) {
                   message = value.message;
                }
            });

           return message;
        }

      

        function Load()
        {
            messages.push({ code: 1, message: 'Data Berhasil Ditambah' });
            messages.push({ code: 1, message: 'Data Berhasil Diubah' });
            messages.push({ code: 401, message: 'Anda tidak memiliki hak akses' });
        }

        return service;

    })

    .factory("PejabatService", function ($http, $q, BaseUrl,Helpers) {
        var service = {};
       var isInstant = false;
       var collection = [];
        service.source = function ()
        {
            deferred = $q.defer();
            if (!isInstant)
            {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/pejabat/get",
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    collection = response.data;
                    deferred.resolve(collection);
                    isInstant = true;
                    }, function (error) {
                        alert(Helpers.getMessage(error.status,error.data.Message));
                   // deferred.reject(error);
                });
               
            } else
            {
                deferred.resolve(collection);
            }

            return deferred.promise;
        }
        service.GetPejabatRW = function ()
        {
            var datas = [];
            if (!isInstant)
            {
               this.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level === 'RW') {
                            datas.push(value);
                        }
                    });

                  
                });
             
            }
            else
            {
                var datas = [];
                angular.forEach(collection, function (value, key) {
                    if (value.Level === 'RW') {
                        datas.push(value);
                    }
                });
               
            }

            return datas;

        }

        service.GetPejabatRT = function () {
            if (!service.isInstant)
                service.source();

            var datas = [];
            angular.forEach(service.collection, function (value, key) {
                if (value.Level == 'RT') {
                    datas.push(value);
                }
            });

        }

        service.Insert = function (model)
        {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.URL + "/api/account/register",
                data: model
            }).then(function (response) {
                collection.push(response.data);
                alert(Helpers.getMessage(1, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        service.Update = function (model) {
            deferred = $q.defer();
            $http({
                method: 'put',
                url: BaseUrl.URL + "/api/pejabat/put",
                data: model
            }).then(function (response) {
                service.collection.push(response.data);
                alert(Helpers.getMessage(2, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        return service;
    })

    .factory("StrukturKelurahanService", function ($http, $q, BaseUrl, Helpers) {
        var service = {};
        var isInstant = false;
        var collection = [];

        service.source = function () {
            deferred = $q.defer();
            if (!isInstant) {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/StrukturKelurahan/getrw",
                }).then(function (data) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    collection = data;
                    deferred.resolve(collection);
                    isInstant = true;

                }, function (error) {

                    alert(Helpers.getMessage(error.status, error.data.Message));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(collection);
            }

            return deferred.promise;
        };

        service.Insert = function(model)
        {
            deferred = $q.defer();
            $http({
                method: 'POST',
                url: BaseUrl.URL + "/api/StrukturKelurahan/postrw",
                data: model
            }).then(function (data) {
                collection.data.push(data);
                deferred.resolve(data);
               
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        return service;
    })




    ;
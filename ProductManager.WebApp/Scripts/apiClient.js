function loadCategories(apiBaseUrl, authorizationKey, url) {
    $.ajax({
        type: "GET",
        url: apiBaseUrl + "ProductCategory/",
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
        },
        success: function (apiData) {
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json",
                data: JSON.stringify({ productCategories: apiData }),
                success: function (data) {
                    $("#categories-dropdown").html(data);
                }
            });
        },
        error: function (apiData) {
            console.log("GET ProductCategory: [ERROR] " + apiData.responseJSON.ExceptionMessage);
        }
    });
}

function loadSubCategories(apiBaseUrl, authorizationKey, url, productSubCategoryId, categoryId) {
    var apiUrl = categoryId ? "ProductSubCategory?categoryId=" + categoryId : "ProductSubCategory";

    $.ajax({
        type: "GET",
        url: apiBaseUrl + apiUrl,
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
        },
        success: function (apiData) {
            if (!productSubCategoryId)
                productSubCategoryId = 0;
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json",
                data: JSON.stringify({ productSubCategories: apiData, productSubCategoryId: productSubCategoryId }),
                success: function (data) {
                    $("#subCategories-dropdown").html(data);
                }
            });
        },
        error: function (apiData) {
            console.log("GET ProductSubCategory: [ERROR] " + apiData.responseJSON.ExceptionMessage);
        }
    });
}

function loadProductList(apiBaseUrl, authorizationKey, url, subCategoryId) {
    $.ajax({
        type: "GET",
        url: apiBaseUrl + "Product?subCategoryId=" + subCategoryId,
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
        },
        success: function (apiData) {
            if (apiData) {
                $.ajax({
                    type: "POST",
                    url: url,
                    contentType: "application/json",
                    data: JSON.stringify({ products: apiData }),
                    success: function (data) {
                        $("#productList").html(data);
                    }
                });
            }
        },
        error: function (apiData) {
            console.log("GET Product: [ERROR] " + apiData.responseJSON.ExceptionMessage);
        }
    });
}

function getProduct(apiBaseUrl, authorizationKey, id, urlProduct, urlSubCategories) {
    $.ajax({
        type: "GET",
        url: apiBaseUrl + "Product?productId=" + id,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
        },
        success: function (apiData) {
            $.ajax({
                type: "POST",
                url: urlProduct,
                contentType: "application/json",
                data: JSON.stringify({ product: apiData }),
                success: function (data) {
                    $("#product").html(data);

                    // Bind the subcategories
                    loadSubCategories(apiBaseUrl, authorizationKey, urlSubCategories, apiData.ProductSubcategoryId);
                },
                error: function(data) {
                }
            });
        },
        error: function (apiData) {
            console.log("GET Product: [ERROR] " + apiData.responseJSON.ExceptionMessage);
        }
    });
}

function createProduct(apiBaseUrl, authorizationKey, form) {
    if (form.valid()) {
        var product = {
            Key: form.serializeArray()[1].value,
            ProductSubCategoryId: form.serializeArray()[2].value,
            Name: form.serializeArray()[3].value,
            StockLevel: form.serializeArray()[4].value,
            Price: form.serializeArray()[5].value
        }

        $.ajax({
            type: "POST",
            url: apiBaseUrl + "Product",
            data: JSON.stringify(product),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
            },
            success: function (apiData) {
                document.location.href = "/";
            },
            error: function (apiData) {
                console.log("POST ProductCategory: [ERROR] " + apiData.responseJSON.ExceptionMessage);
            }
        });
    }
    return false;
}

function updateProduct(apiBaseUrl, authorizationKey, form) {
    if (form.valid()) {
        var product = {
            Id: form.serializeArray()[0].value,
            Key: form.serializeArray()[1].value,
            ProductSubCategoryId: form.serializeArray()[2].value,
            Name: form.serializeArray()[3].value,
            StockLevel: form.serializeArray()[4].value,
            Price: form.serializeArray()[5].value
        }

        $.ajax({
            type: "PUT",
            url: apiBaseUrl + "Product",
            data: JSON.stringify(product),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
            },
            success: function (apiData) {
                document.location.href = "/";
            },
            error: function (apiData) {
                console.log("POST ProductCategory: [ERROR] " + apiData.responseJSON.ExceptionMessage);
            }
        });
    }
    return false;
}

function deleteProduct(apiBaseUrl, authorizationKey, id) {
    $.ajax({
        type: "DELETE",
        url: apiBaseUrl + "Product?id=" + id,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + authorizationKey);
        },
        success: function (apiData) {
            document.location.href = "/";
        },
        error: function (apiData) {
            console.log("POST ProductCategory: [ERROR] " + apiData.responseJSON.ExceptionMessage);
        }
    });
}
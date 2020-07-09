$(function () {   
    var l = abp.localization.getResource("Zero");
	var storeService = window.zero.controllers.stores.store;
	
	
		
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Stores/CreateModal",
        scriptUrl: "/Pages/Stores/createModal.js",
        modalClass: "storeCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Stores/EditModal",
        scriptUrl: "/Pages/Stores/editModal.js",
        modalClass: "storeEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),	
            name: $("#NameFilter").val(),
			url: $("#UrlFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };
	
    var dataTable = $("#StoresTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(storeService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Zero.Stores.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Zero.Stores.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    storeService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" }
,			{ data: "url" }
,            {
                data: "isActive",
                render: function (isActive) {
                    return isActive ? l("Yes") : l("No");
                }
            }

            
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewStoreButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });
	
    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
});
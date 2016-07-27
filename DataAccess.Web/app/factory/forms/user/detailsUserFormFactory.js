angular.module('DataAccessApp').factory('detailsUserFormFactory',
['roleDefinitionProvider', 'userFormFactory', DetailsUserFormFactory]);

function DetailsUserFormFactory(roleDefinitionProvider, userFormFactory) {

    var _fieldModeMapping = [
        {
            field: 'password',
            mode: 'create'
        },
    ];

    var _createLocalFieldEntries = function () {

        var _fieldEntries = [
            {
                insertAfter: 'phoneNumber',
                field: {
                    field: 'roleId',
                    label: 'user.role',
                    optionLabel: 'user.selectRole',
                    attrs: {
                        type: 'select',
                        name: 'roleId',
                        required: '',
                    },
                    dataSource: {
                        static: true,
                        data: roleDefinitionProvider.getAll(),
                    },
                    options: {
                        dataTextField: 'name',
                        dataValueField: 'identifier',
                    },
                    messages: [
                        {
                            validation: 'required',
                            text: 'user.roleIsRequired'
                        },
                    ],
                }
            },
        ];

        return _fieldEntries;
    }

    var _create = function (emailValidator) {

        var fields = userFormFactory.create(emailValidator);
        var localEntries = _createLocalFieldEntries();

        localEntries.forEach(function (entry) {

            var previousFieldIndex = fields.findIndex(function (item) {
                return item.field === entry.insertAfter;
            });

            var targetIndex = previousFieldIndex + 1;

            fields.splice(targetIndex, 0, entry.field);
        });

        _fieldModeMapping.forEach(function (mapping) {

            var targetField = fields.find(function (item) {
                return item.field === mapping.field;
            });

            targetField.mode = mapping.mode;
        });

        return fields;
    }

    return {
        create: _create,
    }
}

'use strict'
angular.module('DataAccessApp').factory('crudColumnFactory',
['actionColumnFactory', 'buttonTemplateFactory', CrudColumnFactory]);

function CrudColumnFactory(actionColumnFactory, buttonTemplateFactory) {

    var _actionDefinitions = {
        'edit': {
            templateFactory: function (attrs) {

                return buttonTemplateFactory.create('circle', 'edit', { type: 'html', value: '<i class="fa fa-pencil"></i>' }, attrs);
            },
            clickCall: "showEdit(dataItem.id)",
        },
        'delete': {
            templateFactory: function (attrs) {

                return buttonTemplateFactory.create('circle', 'delete', { type: 'html', value: '<i class="fa fa-times"></i>' }, attrs);
            },
            clickCall: "showDelete(dataItem.id)",
        },
    };

    var _createActionColumn = function (actionDefinition) {
        return actionColumnFactory.create(
            { template: actionDefinition.template },
            actionDefinition.clickCall
            );
    }

    var _createActionCommand = function (actionName, attrs) {

        var actionDefinition = _actionDefinitions[actionName];

        if (actionDefinition === undefined) {
            console.log(String.format('Unrecognized action: {0}', actionName));
            return undefined;
        }

        var constructedActionDefinition = {
            template: actionDefinition.templateFactory(attrs),
            clickCall: actionDefinition.clickCall,
        }

        return _createActionColumn(constructedActionDefinition).command;
    }

    var _create = function (actions) {

        return {
            command:
                actions.map(function (action) {

                    var actionCommand = typeof (action) === 'string' || action instanceof String
                        ? _createActionCommand(action)
                        : _createActionCommand(action.name, action.attrs);

                    return actionCommand;
                })
                .filter(function (action) {
                    return action !== undefined
                        && action.template !== undefined;
                }),
            attributes: {
                "class": "crud-column",
            }
        };
    };

    return {
        create: _create
    };
}

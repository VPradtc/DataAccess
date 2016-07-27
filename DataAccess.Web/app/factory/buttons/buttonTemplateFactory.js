'use strict'
angular.module('DataAccessApp').factory('buttonTemplateFactory', ButtonTemplateFactory);

function ButtonTemplateFactory() {

    var _templates = {
        'default': '<button class="btn {0}" style="margin-left: 5px;" {1} {3}>{2}</button>',
        'circle': '<button class="btn btn-primary btn-circle {0}" {1} {3}>{2}</button>'
    }

    var _classes = {
        'warning': 'btn-warning',
        'primary': 'btn-primary',
        'details': 'btn-column-details',
        'edit': 'btn-column-edit',
        'delete': 'btn-warning btn-column-delete',
    }

    var _createAttrsString = function (attrsObject) {

        var attributeTemplate = '{0}={1}';
        var result = '';

        angular.forEach(attrsObject, function (value, key) {

            result += String.format(attributeTemplate, key, JSON.stringify(value));
        })

        return result;
    }

    var _createTemplate = function (name, classes, content, attrs) {

        var targetTemplate = _templates[name];

        if(targetTemplate === undefined){
            return undefined;
        }

        var text = content.type === 'text' && String.format('i18n="{0}"', content.value) || '';
        var innerHtml = content.type === 'html' && content.value || '';

        var classCollection = Array.isArray(classes) ? classes : [classes];
        var cssClasses = classCollection.map(
            function (item) {
                return _classes[item] || item;
            }).join(' ');

        var attrsString = attrs === undefined
            ? undefined
            : _createAttrsString(attrs);

        return String.format(targetTemplate, cssClasses, text, innerHtml, attrsString);
    }

    return {
        create: _createTemplate,
    };
}

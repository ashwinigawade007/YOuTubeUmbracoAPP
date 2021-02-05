﻿using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Models;
using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;


namespace BusinessLogic
{
    /// <summary>
    /// The YouTube property value converter.
    /// </summary>
    [PropertyValueType(typeof(global::BusinessLogic.Models.SimpleYouTube))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class SimpleYouTubeValueConverter : IPropertyValueConverter
    {
        /// <summary>
        /// Checks if this converter can convert the property editor and registers if it can.
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("YouTube.Channel");
        }

        /// <summary>
        /// Convert the JSON data into a array
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <param name="source">
        /// The value of the property
        /// </param>
        /// <param name="preview">
        /// The preview.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source != null)
            {
                var selectedVideoList = JsonConvert.DeserializeObject<List<SelectedVideo>>(source.ToString());
                return selectedVideoList;
            }

            return new string[0];
        }

        /// <summary>
        /// Convert the source array into a Video object
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <param name="source">
        /// The value of the property
        /// </param>
        /// <param name="preview">
        /// The preview.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            var selectedVideos = new SimpleYouTube(source as List<SelectedVideo>);
            return selectedVideos;
        }

        /// <summary>
        /// Convert the source array into a CSV string
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <param name="source">
        /// The value of the property
        /// </param>
        /// <param name="preview">
        /// The preview.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            var selectedVideos = (SimpleYouTube)source;
            var selectedVideoIds = selectedVideos.Select(v => v.Id);
            return string.Join(",", selectedVideoIds);
        }
    }
}


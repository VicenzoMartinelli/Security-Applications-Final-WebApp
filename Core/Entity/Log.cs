﻿using Core.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entity
{
  public class Log  
  {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Url { get; set; }
    public string RequestIp { get; set; }
    public string RequestProtocol { get; set; }
    public string RequestMethod { get; set; } 
    [JsonConverter(typeof(StringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public ELogType Type { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
  }
}

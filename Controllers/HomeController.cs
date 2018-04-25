using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pokeinfo.Models;
using Newtonsoft.Json.Linq;

namespace pokeinfo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult QueryPoke(int pokeid)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                }
            ).Wait();

            JArray pokeTypes = (JArray)PokeInfo["types"];
            List<string> types = new List<string>();
            for(int i =0; i < pokeTypes.Count; i++)
            {
                types.Add((string)pokeTypes[i]["type"]["name"]);
            }
            JObject sprites = (JObject)PokeInfo["sprites"];

            ViewBag.types = types;
            ViewBag.sprite = sprites["front_default"];
            ViewBag.name = (string)PokeInfo["name"];
            ViewBag.height = (long)PokeInfo["height"];
            ViewBag.weight = (long)PokeInfo["weight"];
            return View();
        }
    }
}

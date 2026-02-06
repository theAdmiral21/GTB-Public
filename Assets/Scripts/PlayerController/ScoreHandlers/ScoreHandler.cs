// using Assets.Scripts.Channels.DataStructures;
// using Assets.Scripts.LevelObjects.Items;
// using UnityEngine;

// namespace Assets.Scripts.PlayerController.ScoreHandlers
// {
//     public class ScoreHandler
//     {
//         private readonly PlayerHost _host;
//         public ScoreHandler(PlayerHost host)
//         {
//             _host = host;
//         }

//         public void AddScore(int points)
//         {
//             _host.SessionData.PlayerStatusData.PlayerScore += points;
//             _host.Hud.ScoreChanged?.Invoke($"{_host.SessionData.PlayerStatusData.PlayerScore}");
//         }

//         public void PickUpDino(PickUpContext context)
//         {
//             // Update the hud
//             _host.Hud.DinoFound?.Invoke(context.Index);
//         }
//     }
// }
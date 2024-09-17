using EventManager;
using EventManager.Model;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionController : MonoBehaviour
{
    [SerializeField]
    private Transform m_player;
    [SerializeField]
    private List<GameObject> List;

    private bool m_disable;

    private void Start()
    {
        List = GetComponent<SpawnController>().List;
        m_disable = true;
        EventBus.Instance.StartListening(EventBusType.DISABLE_COLLISION, DisableCollision);
        EventBus.Instance.StartListening(EventBusType.ENABLE_COLLISION, EnableCollision);
    }

    private void Update()
    {
        if (m_disable)
            return;

        foreach (GameObject go in List)
        {
            var topLeftSide = new BlockCollision
            {
                Start = new Vector3(go.transform.position.x, BlockConst.TOP_START_POS_Y, 0),
                End = new Vector3(go.transform.position.x, BlockConst.TOP_START_POS_Y + go.GetComponent<Block>().TopLength * BlockConst.TOP_OFFSET_Y, 0),
                Point = m_player.position
            };

            var topBotSide = new BlockCollision
            {
                Start = new Vector3(go.transform.position.x, BlockConst.TOP_START_POS_Y + go.GetComponent<Block>().TopLength * BlockConst.TOP_OFFSET_Y, 0),
                End = new Vector3(go.transform.position.x, BlockConst.TOP_START_POS_Y + go.GetComponent<Block>().TopLength * BlockConst.TOP_OFFSET_Y, 0),
                Point = m_player.position
            };

            var downLeftSide = new BlockCollision
            {
                Start = new Vector3(go.transform.position.x, BlockConst.DOWN_START_POS_Y, 0),
                End = new Vector3(go.transform.position.x, BlockConst.DOWN_START_POS_Y + go.GetComponent<Block>().DownLength * BlockConst.DOWN_OFFSET_Y, 0),
                Point = m_player.position
            };

            var downTopSide = new BlockCollision
            {
                Start = new Vector3(go.transform.position.x, BlockConst.DOWN_START_POS_Y + go.GetComponent<Block>().DownLength * BlockConst.DOWN_OFFSET_Y, 0),
                End = new Vector3(go.transform.position.x, BlockConst.DOWN_START_POS_Y + go.GetComponent<Block>().DownLength * BlockConst.DOWN_OFFSET_Y, 0),
                Point = m_player.position
            };

            var score = new BlockCollision
            {
                Start = new Vector3(go.transform.position.x, BlockConst.TOP_START_POS_Y - 1 + go.GetComponent<Block>().TopLength * BlockConst.TOP_OFFSET_Y, 0),
                End = new Vector3(go.transform.position.x, BlockConst.DOWN_START_POS_Y + 1 + go.GetComponent<Block>().DownLength * BlockConst.DOWN_OFFSET_Y, 0),
                Point = m_player.position,
            };

            if(topLeftSide.IsCollide || topBotSide.IsCollide || downLeftSide.IsCollide || downTopSide.IsCollide)
            {
                EventBus.Instance.FireEvent(EventBusType.PLAY_HURT_ANIMATION);
                EventBus.Instance.FireEvent(EventBusType.SHOW_HIT_EFFECT_PLAYER);
                EventBus.Instance.FireEvent(EventBusType.DISABLE_CONTROL_PLAYER);
                EventBus.Instance.FireEvent(EventBusType.RESTART_GAME);
                SoundManager.Instance.Play(SoundType.HIT);
            }

            if (score.IsCollide && !go.GetComponent<Block>().IsScore)
            {
                ScoreManager.Instance.Add();
                ScoreManager.Instance.Display();
                go.GetComponent<Block>().IsScore = true;
                SoundManager.Instance.Play(SoundType.SCORE);
            }

            if (!score.IsCollide && go.GetComponent<Block>().IsScore)
            {
                go.GetComponent<Block>().IsScore = false;
            }
        }
    }

    private void DisableCollision()
    {
        m_disable = true;
    }

    private void EnableCollision()
    {
        m_disable = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using BOM;
using Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Kenshi
{
    /// <summary>
    /// 건축 순서
    /// 건축 메뉴 -> 카테고리 선택 -> 건물 선택
    /// 건물이 선택된경우 -> 마우스 포지션에 건물 프리뷰 메쉬 생성
    /// C: 건물이 가진 Bound Data로 건축 가능 불가능, 각도 (기울기) 수정
    /// C: 건물 높이가 지면과 얼마나 맞닿아 있는지 계산
    /// C: 다른 주변 건물과 충돌하는지 계산
    ///
    /// 건축을 위해 클릭한경우 -> 건축 진입
    /// 건축물 타입은 Point Dot, Point Line (start, end), Dot은 회전가능 Line은 불가능 주로 울타리 건축이나 벽 건축, 건물 건축으로 구분짓기 위해 사용
    /// </summary>
    public partial class BuildingConstructManager : SingletonBehaviour<BuildingConstructManager>
    {      
        public enum BuildingState
        {
            None,
            BuildingMenu,
            SelectCategory,
            SelectedBuildingItem,
            SetBuildingPos,
            SetLink,
            CreateBluePrint,
            Complete
        }

        public class BuildingDataContext
        {
        }

        public struct BuildData
        {
            public int buildingId;
            public int actionId;
            public Vector3 selectPosition;
        }  
       
    }

    public class BuildingManagerRuntimeController : AbstractBuildingController
    { 
    }

    public partial class BuildingConstructManager : SingletonBehaviour<BuildingConstructManager>
    {
        [SerializeField] private CinemachineVirtualCamera buildingCamera; 
        public IBuildingConstructController Controller { get; set; } 
        public void Initialize(IBuildingConstructController controller)
        {
            this.Controller = controller;
        }  
        public Ray GetRaycastHitPoint(Camera camera, Vector2 screenPoint)
        {
            var ray = camera.ScreenPointToRay(screenPoint);
            return ray;
        }
    }
}
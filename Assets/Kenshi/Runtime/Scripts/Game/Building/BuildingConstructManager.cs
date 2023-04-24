 
using System;
using BOM;
using Cinemachine;
using Shapes;
using UnityEngine; 
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
            Wait = 0,
            BuildingMenu ,
            SelectCategory,
            SelectedBuildingItem,
            /// <summary>
            /// Preview Building Mesh
            /// </summary>
            WaitPlayerSetToBuildingPoint,
            /// <summary>
            /// Preview Building Mesh And Lines
            /// </summary>
            WaitPlayerSetToBuildingLines,
            WaitPlayerAgreeInput,
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
 
    public partial class BuildingConstructManager : SingletonBehaviour<BuildingConstructManager>
    {
        [SerializeField] private BuildingState state;
        [SerializeField] private CinemachineVirtualCamera buildingCamera;

        private IBuildingConstructController _controller;
        public IBuildingConstructController Controller
        {
            get
            {
                if (_controller == null && Application.isPlaying)
                {
                    _controller = new BuildingManagerRuntimeController();
                }
                return _controller;
            }
            set
            {
                _controller = value;
            }
        } 
        public void StartBuilding()
        {
            if (state != BuildingState.Wait) return;  
        }

        public void Update()
        { 
            var test = Controller.RaycastAbovePoint(Input.mousePosition);
            if (test.HasValue)
            {
                var entity = test.Value.collider.GetComponentInParent<BuildingEntity>();
                Debug.Log(entity.name); 
            }
            else
            {
                Debug.Log("Sad");
            }
        }
    }
}
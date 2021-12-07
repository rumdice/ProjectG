# ProjectG

인프라 : AWS EC2 (amazone linux), AWS RDS

엔진 : Kestrel (.net 기본)

언어 : C#

DB : mariaDB(mysql), redis

기타 : docker, grpc


# 구성
GrpcClient : 테스트 클라이언트

GrpcServer : 서버

Protocol : protobuf 형식의 패킷 파일

Script : 서버 설정 및 실행에 필요한 bash 스크립트

Table : 게임 데이터 테이블 폴더 (.csv)



# branch
dev : 개발 브랜치

master : 보관용 브랜치 (live)

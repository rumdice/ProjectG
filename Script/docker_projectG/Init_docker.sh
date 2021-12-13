#!/bin/bash
# 사용자 pc에 docker desktop 설치 여부 확인
# 같은 PC에 같은 port를 중복해서 쓸 수 없어서 일반적인 사용 포트와 조금 다름
# mysql : (3306)
# redis : (6379)


# 옵션 설명 
# -f (강제 실행)
# up (컨테이너 생성 실행)
# -d (deamon 백그라운드에서 실행)
# --build (이미지 빌드)

# redis
docker-compose -f redis_docker_compose.yml up -d --build

# mariadb
docker-compose -f mariadb_docker_compose.yml up -d --build
